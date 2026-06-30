import { NextResponse } from 'next/server';
import { revalidatePath } from 'next/cache';

export async function POST(request: Request) {
  try {
    const body = await request.json();
    const { secret, slug } = body;

    const revalidateSecret = process.env.REVALIDATE_SECRET;
    if (!revalidateSecret || secret !== revalidateSecret) {
      return NextResponse.json({ message: 'Unauthorized' }, { status: 401 });
    }

    const pathsToRevalidate = ['/', '/gioi-thieu', '/dich-vu', '/du-an', '/tin-tuc', '/lien-he'];
    if (slug) pathsToRevalidate.push(slug);

    await Promise.all(pathsToRevalidate.map(path => revalidatePath(path)));

    return NextResponse.json({ revalidated: true, now: Date.now() }, { status: 200 });
  } catch {
    return NextResponse.json({ message: 'Revalidation failed' }, { status: 500 });
  }
}
