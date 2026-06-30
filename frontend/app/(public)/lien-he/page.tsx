'use client';

import { useState, useCallback } from 'react';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';

export default function ContactPage() {
  const [formData, setFormData] = useState({ fullName: '', phone: '', email: '', need: '', budget: '', message: '' });
  const [submitted, setSubmitted] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = useCallback(async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Validation
    if (!formData.fullName.trim() || !formData.phone.trim() || !formData.need) {
      setError('Vui lòng điền đầy đủ thông tin bắt buộc (Họ tên, Số điện thoại, Nhu cầu)');
      return;
    }

    // Phone format validation
    const phoneRegex = /^[0-9]{9,11}$/;
    const cleanPhone = formData.phone.replace(/[\s.-]/g, '');
    if (!phoneRegex.test(cleanPhone)) {
      setError('Số điện thoại không hợp lệ (9-11 chữ số)');
      return;
    }

    // Email format validation (if provided)
    if (formData.email.trim()) {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(formData.email.trim())) {
        setError('Email không hợp lệ');
        return;
      }
    }

    setLoading(true);

    try {
      const res = await fetch('/api/public/contact', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData),
      });

      const data = await res.json();

      if (!res.ok) {
        setError(data.message || 'Có lỗi xảy ra, vui lòng thử lại sau');
        return;
      }

      setSubmitted(true);
    } catch (err) {
      setError('Không thể kết nối đến server. Vui lòng thử lại sau.');
    } finally {
      setLoading(false);
    }
  }, [formData]);

  if (submitted) {
    return (
      <>
        <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
          <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
          <div className="relative z-10 container-custom text-center text-white">
            <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Liên Hệ</p>
            <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl">Liên Hệ Với Chúng Tôi</h1>
          </div>
        </section>
        <Section>
          <div className="container-custom">
            <div className="max-w-2xl mx-auto text-center">
              <div className="bg-brand-cream p-12">
                <svg className="w-16 h-16 text-brand-gold mx-auto mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
                <h3 className="font-heading text-2xl mb-2">Cảm Ơn Bạn!</h3>
                <p className="font-body text-brand-dark/60">Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất.</p>
              </div>
            </div>
          </div>
        </Section>
      </>
    );
  }

  return (
    <>
      <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Liên Hệ</p>
          <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl">Liên Hệ Với Chúng Tôi</h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-16">
            <div>
              <SectionHeader subtitle="Thông Tin Liên Hệ" title="Hãy Để Chúng Tôi Giúp Bạn" align="left" />
              <p className="font-body text-brand-dark/70 text-base leading-relaxed mb-10">
                Đừng ngần ngại liên hệ với chúng tôi để nhận tư vấn miễn phí. Đội ngũ chuyên gia của An Homes luôn sẵn sàng hỗ trợ bạn.
              </p>
              <div className="space-y-6">
                <div className="flex items-start gap-4">
                  <div className="w-12 h-12 bg-brand-gold/10 flex items-center justify-center shrink-0">
                    <svg className="w-5 h-5 text-brand-gold" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"/><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"/></svg>
                  </div>
                  <div>
                    <h4 className="font-heading text-sm mb-1">Địa Chỉ</h4>
                    <p className="font-body text-sm text-brand-dark/60">HCMC, Việt Nam</p>
                  </div>
                </div>
                <div className="flex items-start gap-4">
                  <div className="w-12 h-12 bg-brand-gold/10 flex items-center justify-center shrink-0">
                    <svg className="w-5 h-5 text-brand-gold" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z"/></svg>
                  </div>
                  <div>
                    <h4 className="font-heading text-sm mb-1">Điện Thoại</h4>
                    <a href={`tel:${process.env.NEXT_PUBLIC_PHONE || '0909xxx'}`} className="font-body text-sm text-brand-dark/60 hover:text-brand-gold transition-colors cursor-pointer">{process.env.NEXT_PUBLIC_PHONE || '0909.xxx.xxx'}</a>
                  </div>
                </div>
                <div className="flex items-start gap-4">
                  <div className="w-12 h-12 bg-brand-gold/10 flex items-center justify-center shrink-0">
                    <svg className="w-5 h-5 text-brand-gold" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 002 2v10a2 2 0 002 2z"/></svg>
                  </div>
                  <div>
                    <h4 className="font-heading text-sm mb-1">Email</h4>
                    <a href="mailto:contact@noithatanhomes.vn" className="font-body text-sm text-brand-dark/60 hover:text-brand-gold transition-colors cursor-pointer">contact@noithatanhomes.vn</a>
                  </div>
                </div>
                <div className="flex items-start gap-4">
                  <div className="w-12 h-12 bg-brand-gold/10 flex items-center justify-center shrink-0">
                    <svg className="w-5 h-5 text-brand-gold" fill="currentColor" viewBox="0 0 24 24"><path d="M12 0C5.373 0 0 5.373 0 12s5.373 12 12 12 12-5.373 12-12S18.627 0 12 0zm5.894 8.221l-1.97 9.28c-.145.658-.537.818-1.084.508l-3-2.21-1.446 1.394c-.14.18-.357.295-.6.295l.213-3.053 5.56-5.023c.242-.213-.054-.334-.373-.121l-6.869 4.326-2.96-.924c-.64-.203-.654-.64.135-.954l11.566-4.458c.537-.194 1.006.131.832.94z"/></svg>
                  </div>
                  <div>
                    <h4 className="font-heading text-sm mb-1">Zalo</h4>
                    <a href={process.env.NEXT_PUBLIC_ZALO || 'https://zalo.me/0909xxx'} target="_blank" rel="noopener noreferrer" className="font-body text-sm text-brand-dark/60 hover:text-brand-gold transition-colors cursor-pointer">{process.env.NEXT_PUBLIC_PHONE || '0909.xxx.xxx'}</a>
                  </div>
                </div>
              </div>
            </div>
            <div>
              {error && (
                <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 mb-6" role="alert">
                  <p className="font-body text-sm">{error}</p>
                </div>
              )}
              <form onSubmit={handleSubmit} className="bg-brand-cream p-8 md:p-10">
                <h3 className="font-heading text-xl mb-6">Gửi Yêu Cầu Tư Vấn</h3>
                <div className="space-y-5">
                  <div>
                    <label htmlFor="fullName" className="font-body text-sm text-brand-dark/70 block mb-2">Họ và tên *</label>
                    <input id="fullName" type="text" required value={formData.fullName} onChange={e => setFormData({...formData, fullName: e.target.value})} disabled={loading} className="w-full px-4 py-3 border border-gray-200 bg-white font-body text-sm focus:outline-none focus:border-brand-gold transition-colors disabled:opacity-50 disabled:cursor-not-allowed" placeholder="Nhập họ và tên" />
                  </div>
                  <div>
                    <label htmlFor="phone" className="font-body text-sm text-brand-dark/70 block mb-2">Số điện thoại *</label>
                    <input id="phone" type="tel" required value={formData.phone} onChange={e => setFormData({...formData, phone: e.target.value})} disabled={loading} className="w-full px-4 py-3 border border-gray-200 bg-white font-body text-sm focus:outline-none focus:border-brand-gold transition-colors disabled:opacity-50 disabled:cursor-not-allowed" placeholder="Nhập số điện thoại" />
                  </div>
                  <div>
                    <label htmlFor="email" className="font-body text-sm text-brand-dark/70 block mb-2">Email</label>
                    <input id="email" type="email" value={formData.email} onChange={e => setFormData({...formData, email: e.target.value})} disabled={loading} className="w-full px-4 py-3 border border-gray-200 bg-white font-body text-sm focus:outline-none focus:border-brand-gold transition-colors disabled:opacity-50 disabled:cursor-not-allowed" placeholder="Nhập email (tùy chọn)" />
                  </div>
                  <div>
                    <label htmlFor="need" className="font-body text-sm text-brand-dark/70 block mb-2">Nhu cầu *</label>
                    <select id="need" required value={formData.need} onChange={e => setFormData({...formData, need: e.target.value})} disabled={loading} className="w-full px-4 py-3 border border-gray-200 bg-white font-body text-sm focus:outline-none focus:border-brand-gold transition-colors disabled:opacity-50 disabled:cursor-not-allowed">
                      <option value="">Chọn dịch vụ</option>
                      <option value="Thiết kế nội thất">Thiết kế nội thất</option>
                      <option value="Thi công nội thất">Thi công nội thất</option>
                      <option value="Thiết kế kiến trúc">Thiết kế kiến trúc</option>
                      <option value="Tư vấn nội thất">Tư vấn nội thất</option>
                    </select>
                  </div>
                  <div>
                    <label htmlFor="budget" className="font-body text-sm text-brand-dark/70 block mb-2">Ngân sách</label>
                    <input id="budget" type="text" value={formData.budget} onChange={e => setFormData({...formData, budget: e.target.value})} disabled={loading} className="w-full px-4 py-3 border border-gray-200 bg-white font-body text-sm focus:outline-none focus:border-brand-gold transition-colors disabled:opacity-50 disabled:cursor-not-allowed" placeholder="Ví dụ: 500 triệu - 1 tỷ" />
                  </div>
                  <div>
                    <label htmlFor="message" className="font-body text-sm text-brand-dark/70 block mb-2">Nội dung chi tiết</label>
                    <textarea id="message" rows={4} value={formData.message} onChange={e => setFormData({...formData, message: e.target.value})} disabled={loading} className="w-full px-4 py-3 border border-gray-200 bg-white font-body text-sm focus:outline-none focus:border-brand-gold transition-colors resize-none disabled:opacity-50 disabled:cursor-not-allowed" placeholder="Mô tả chi tiết nhu cầu của bạn..." />
                  </div>
                  <button type="submit" disabled={loading} className="btn-primary w-full disabled:opacity-50 disabled:cursor-not-allowed">
                    {loading ? 'Đang gửi...' : 'Gửi Yêu Cầu'}
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </Section>
    </>
  );
}
