import { Metadata } from 'next';
import Section from '@/components/shared/Section';

type Props = { params: Promise<{ slug: string }> };

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const { slug } = await params;
  return {
    title: `${decodeURIComponent(slug).replace(/-/g, ' ')} | An Homes`,
    description: 'Bài viết chi tiết từ An Homes.',
  };
}

export default async function PostDetailPage({ params }: Props) {
  const { slug } = await params;
  const title = decodeURIComponent(slug).replace(/-/g, ' ').replace(/\b\w/g, c => c.toUpperCase());

  return (
    <>
      <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Bài Viết</p>
          <h1 className="font-heading text-3xl md:text-4xl lg:text-5xl max-w-4xl">{title}</h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <div className="max-w-3xl mx-auto">
            <div className="aspect-video bg-gradient-to-br from-neutral-200 to-neutral-300 mb-12" />
            <div className="prose prose-lg max-w-none">
              <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-6">
                Nội dung bài viết đang được cập nhật. Vui lòng quay lại sau hoặc liên hệ với chúng tôi để biết thêm thông tin chi tiết.
              </p>
              <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-6">
                An Homes luôn cam kết mang đến những thông tin hữu ích nhất về thiết kế và thi công nội thất cao cấp. Hãy theo dõi blog của chúng tôi để cập nhật những xu hướng mới nhất và các mẹo thiết kế hữu ích.
              </p>
            </div>
            <div className="mt-12 pt-8 border-t">
              <a href="/tin-tuc" className="btn-outline">Quay Lại Danh Sách</a>
            </div>
          </div>
        </div>
      </Section>
    </>
  );
}
