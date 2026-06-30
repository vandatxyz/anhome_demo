import { NextResponse } from 'next/server';

// Simple in-memory rate limiter (for production, use Redis or Upstash)
const rateLimitMap = new Map<string, { count: number; resetTime: number }>();
const RATE_LIMIT_WINDOW = 60000; // 1 minute
const RATE_LIMIT_MAX = 5; // max 5 requests per minute per IP

function checkRateLimit(ip: string): boolean {
  const now = Date.now();
  const record = rateLimitMap.get(ip);

  if (!record || now > record.resetTime) {
    rateLimitMap.set(ip, { count: 1, resetTime: now + RATE_LIMIT_WINDOW });
    return true;
  }

  if (record.count >= RATE_LIMIT_MAX) {
    return false;
  }

  record.count++;
  return true;
}

function sanitizeInput(input: string): string {
  return input.trim().replace(/[<>]/g, '');
}

export async function POST(request: Request) {
  try {
    // Rate limiting by IP
    const ip = request.headers.get('x-forwarded-for') || request.headers.get('x-real-ip') || 'unknown';
    if (!checkRateLimit(ip)) {
      return NextResponse.json({ message: 'Quá nhiều yêu cầu. Vui lòng thử lại sau 1 phút.' }, { status: 429 });
    }

    const body = await request.json();
    const { fullName, phone, email, need, budget, message } = body;

    // Sanitize inputs
    const sanitizedData = {
      fullName: sanitizeInput(fullName || ''),
      phone: sanitizeInput(phone || ''),
      email: email ? sanitizeInput(email) : '',
      need: sanitizeInput(need || ''),
      budget: budget ? sanitizeInput(budget) : '',
      message: message ? sanitizeInput(message) : '',
    };

    // Validate required fields
    if (!sanitizedData.fullName || !sanitizedData.phone || !sanitizedData.need) {
      return NextResponse.json({ message: 'Vui lòng điền đầy đủ thông tin bắt buộc' }, { status: 400 });
    }

    // Validate phone format
    const phoneRegex = /^[0-9]{9,11}$/;
    const cleanPhone = sanitizedData.phone.replace(/[\s.-]/g, '');
    if (!phoneRegex.test(cleanPhone)) {
      return NextResponse.json({ message: 'Số điện thoại không hợp lệ' }, { status: 400 });
    }

    // Validate email format if provided
    if (sanitizedData.email) {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(sanitizedData.email)) {
        return NextResponse.json({ message: 'Email không hợp lệ' }, { status: 400 });
      }
    }

    // TODO: Save to database via backend API
    // TODO: Send email notification via SMTP/Resend/SendGrid
    console.log('Contact form submission:', { ...sanitizedData, ip });

    return NextResponse.json({ id: `contact-${Date.now()}`, message: 'Gửi thành công' }, { status: 200 });
  } catch {
    return NextResponse.json({ message: 'Có lỗi xảy ra, vui lòng thử lại sau' }, { status: 500 });
  }
}
