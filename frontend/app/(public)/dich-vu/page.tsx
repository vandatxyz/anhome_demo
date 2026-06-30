import { Metadata } from 'next';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';

export const metadata: Metadata = {
  title: 'Dịch Vụ - An Homes',
  description: 'Dịch vụ thiết kế và thi công nội thất cao cấp - An Homes. Giải pháp toàn diện từ thiết kế đến thi công.',
  keywords: ['dịch vụ nội thất', 'thiết kế nội thất', 'thi công nội thất', 'tư vấn nội thất', 'thiết kế kiến trúc'],
  alternates: { canonical: '/dich-vu' },
};

const services = [
  { title: 'Thiết Kế Nội Thất', slug: 'thiet-ke-noi-that', desc: 'Giải pháp thiết kế nội thất luxury, tối giản, phù hợp với từng không gian sống.', features: ['Thiết kế 2D/3D chuyên nghiệp', 'Lựa chọn vật liệu cao cấp', 'Báo giá chi tiết minh bạch', 'Tư vấn phong thủy không gian'] },
  { title: 'Thi Công Nội Thất', slug: 'thi-cong-noi-that', desc: 'Thi công trọn gói với đội ngũ thợ lành nghề, vật liệu cao cấp, đảm bảo tiến độ.', features: ['Thi công trọn gói A-Z', 'Giám sát chặt chẽ từng hạng mục', 'Bảo hành 2-5 năm', 'Hoàn thiện chi tiết tinh xảo'] },
  { title: 'Thiết Kế Kiến Trúc', slug: 'thiet-ke-kien-truc', desc: 'Thiết kế kiến trúc biệt thự, nhà phố với phong cách hiện đại và sang trọng.', features: ['Thiết kế mặt tiền độc đáo', 'Bản vẽ kỹ thuật chi tiết', 'Giải pháp chiếu sáng tự nhiên', 'Tối ưu công năng'] },
  { title: 'Tư Vấn Nội Thất', slug: 'tu-van-noi-that', desc: 'Tư vấn chuyên sâu về phong cách, vật liệu, màu sắc và bố cục không gian.', features: ['Tư vấn phong thủy', 'Lựa chọn vật liệu phù hợp', 'Phối màu sắc hài hòa', 'Bố cục tối ưu diện tích'] },
];

export default function ServicesPage() {
  return (
    <>
      <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Dịch Vụ</p>
          <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl">Dịch Vụ Của Chúng Tôi</h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <SectionHeader subtitle="Giải Pháp Toàn Diện" title="Từ Thiết Kế Đến Thi Công" description="Chúng tôi cung cấp dịch vụ trọn gói với chất lượng cao cấp nhất." />
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
            {services.map((service) => (
              <Link key={service.slug} href={`/dich-vu/${service.slug}`} className="group block bg-white border border-gray-100 p-8 hover:border-brand-gold/30 hover:shadow-xl transition-all duration-300 cursor-pointer">
                <h3 className="font-heading text-xl mb-3 group-hover:text-brand-gold transition-colors">{service.title}</h3>
                <p className="font-body text-sm text-brand-dark/60 leading-relaxed mb-6">{service.desc}</p>
                <ul className="space-y-2 mb-6">
                  {service.features.map(f => (
                    <li key={f} className="font-body text-sm text-brand-dark/70 flex items-center gap-2">
                      <svg className="w-4 h-4 text-brand-gold shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" /></svg>
                      {f}
                    </li>
                  ))}
                </ul>
                <span className="font-body text-sm text-brand-gold tracking-wider uppercase inline-flex items-center gap-2 group-hover:gap-3 transition-all">
                  Tìm hiểu thêm
                  <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 8l4 4m0 0l-4 4m4-4H3" /></svg>
                </span>
              </Link>
            ))}
          </div>
        </div>
      </Section>
    </>
  );
}
