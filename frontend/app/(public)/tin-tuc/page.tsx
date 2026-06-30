import { Metadata } from 'next';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';

export const metadata: Metadata = {
  title: 'Tin Tức - An Homes',
  description: 'Tin tức, xu hướng thiết kế nội thất và kiến trúc từ An Homes. Cập nhật kiến thức và kinh nghiệm.',
  keywords: ['tin tức nội thất', 'xu hướng nội thất', 'mẹo thiết kế', 'kiến trúc'],
  alternates: { canonical: '/tin-tuc' },
};

const posts = [
  { title: 'Xu Hướng Nội Thất 2025: Tối Giản Sang Trọng', category: 'Xu hướng', date: '15/06/2025', excerpt: 'Khám phá xu hướng nội thất 2025 với phong cách tối giản kết hợp sang trọng, sử dụng vật liệu tự nhiên và ánh sáng.' },
  { title: 'Cách Chọn Vật Liệu Nội Thất Cao Cấp', category: 'Mẹo thiết kế', date: '10/06/2025', excerpt: 'Hướng dẫn chi tiết cách lựa chọn vật liệu nội thất phù hợp với ngân sách và phong cách không gian sống.' },
  { title: 'Phong Thủy Nhà Ở: Những Điều Cần Biết', category: 'Phong thủy', date: '05/06/2025', excerpt: 'Những nguyên tắc phong thủy cơ bản giúp tạo không gian sống hài hòa, may mắn và thịnh vượng.' },
  { title: 'Bí Quyết Thiết Kế Phòng Khách Đẹp', category: 'Mẹo thiết kế', date: '01/06/2025', excerpt: 'Những bí quyết từ chuyên gia để thiết kế phòng khách đẹp, thoáng đãng và đầy tính thẩm mỹ.' },
  { title: 'So Sánh Phong Cách Nội Thất Hiện Đại và Tối Giản', category: 'Xu hướng', date: '28/05/2025', excerpt: 'Tìm hiểu điểm khác biệt giữa phong cách nội thất hiện đại và tối giản để chọn phù hợp với không gian của bạn.' },
  { title: 'Cách Tối Ưu Chi Phí Thi Công Nội Thất', category: 'Mẹo thiết kế', date: '25/05/2025', excerpt: 'Bí quyết tối ưu chi phí mà vẫn đảm bảo chất lượng thi công nội thất cao cấp.' },
];

export default function BlogPage() {
  return (
    <>
      <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Blog</p>
          <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl">Tin Tức & Xu Hướng</h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <SectionHeader subtitle="Kiến Thức Nội Thất" title="Cập Nhật Xu Hướng & Mẹo Thiết Kế" description="Chia sẻ kiến thức, xu hướng và kinh nghiệm từ đội ngũ chuyên gia của An Homes." />
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {posts.map((post, i) => (
              <Link key={post.title} href={`/tin-tuc/${encodeURIComponent(post.title.toLowerCase().replace(/\s+/g, '-'))}`} className="group cursor-pointer">
                <div className="aspect-video bg-gradient-to-br from-neutral-200 to-neutral-300 mb-4 overflow-hidden relative">
                  <div className="absolute inset-0 bg-brand-black/0 group-hover:bg-brand-black/40 transition-all duration-500 flex items-center justify-center">
                    <span className="text-white font-body text-sm tracking-widest uppercase opacity-0 group-hover:opacity-100 transition-opacity duration-300">Đọc thêm</span>
                  </div>
                </div>
                <div className="flex items-center gap-3 mb-3">
                  <span className="font-body text-xs text-brand-gold tracking-wider uppercase">{post.category}</span>
                  <span className="w-1 h-1 bg-brand-dark/30 rounded-full" />
                  <span className="font-body text-xs text-brand-dark/40">{post.date}</span>
                </div>
                <h3 className="font-heading text-base mb-2 group-hover:text-brand-gold transition-colors">{post.title}</h3>
                <p className="font-body text-sm text-brand-dark/60 leading-relaxed">{post.excerpt}</p>
              </Link>
            ))}
          </div>
        </div>
      </Section>
    </>
  );
}
