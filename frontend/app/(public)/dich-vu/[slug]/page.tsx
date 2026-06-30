import { Metadata } from 'next';
import Section from '@/components/shared/Section';
import Link from 'next/link';

type Props = { params: Promise<{ slug: string }> };

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const { slug } = await params;
  return {
    title: `Dịch Vụ - ${slug} | An Homes`,
    description: 'Chi tiết dịch vụ tại An Homes.',
  };
}

const serviceData: Record<string, { title: string; desc: string; content: string; features: string[] }> = {
  'thiet-ke-noi-that': {
    title: 'Thiết Kế Nội Thất',
    desc: 'Giải pháp thiết kế nội thất luxury, tối giản, phù hợp với từng không gian sống.',
    content: 'Đội ngũ kiến trúc sư của An Homes sẽ lắng nghe và thấu hiểu nhu cầu của bạn để tạo ra những bản thiết kế độc đáo, vừa đảm bảo thẩm mỹ vừa tối ưu công năng.',
    features: ['Thiết kế 2D/3D chuyên nghiệp', 'Lựa chọn vật liệu cao cấp', 'Báo giá chi tiết minh bạch', 'Tư vấn phong thủy không gian', 'Điều chỉnh concept theo yêu cầu'],
  },
  'thi-cong-noi-that': {
    title: 'Thi Công Nội Thất',
    desc: 'Thi công trọn gói với đội ngũ thợ lành nghề, vật liệu cao cấp, đảm bảo tiến độ.',
    content: 'Với đội ngũ thợ lành nghề và quy trình kiểm soát chất lượng nghiêm ngặt, An Homes đảm bảo mỗi công trình đều được hoàn thiện đúng tiến độ và vượt mong đợi.',
    features: ['Thi công trọn gói A-Z', 'Giám sát chặt chẽ từng hạng mục', 'Bảo hành 2-5 năm', 'Hoàn thiện chi tiết tinh xảo', 'Vật liệu nhập khẩu chính hãng'],
  },
  'thiet-ke-kien-truc': {
    title: 'Thiết Kế Kiến Trúc',
    desc: 'Thiết kế kiến trúc biệt thự, nhà phố với phong cách hiện đại và sang trọng.',
    content: 'Chúng tôi thiết kế không gian sống vừa đẹp vừa thông minh, tối ưu công năng và hài hòa với môi trường xung quanh.',
    features: ['Thiết kế mặt tiền độc đáo', 'Bản vẽ kỹ thuật chi tiết', 'Giải pháp chiếu sáng tự nhiên', 'Tối ưu công năng', 'Tích hợp smart home'],
  },
  'tu-van-noi-that': {
    title: 'Tư Vấn Nội Thất',
    desc: 'Tư vấn chuyên sâu về phong cách, vật liệu, màu sắc và bố cục không gian.',
    content: 'Dịch vụ tư vấn giúp bạn đưa ra quyết định đúng đắn về phong cách, vật liệu và bố cục không gian trước khi đầu tư.',
    features: ['Tư vấn phong thủy', 'Lựa chọn vật liệu phù hợp', 'Phối màu sắc hài hòa', 'Bố cục tối ưu diện tích', 'Ước tính chi phí chính xác'],
  },
};

export default async function ServiceDetailPage({ params }: Props) {
  const { slug } = await params;
  const service = serviceData[slug] || serviceData['thiet-ke-noi-that'];

  return (
    <>
      <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Dịch Vụ</p>
          <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl">{service.title}</h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <div className="max-w-3xl mx-auto">
            <p className="section-subtitle text-center">{service.title}</p>
            <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-8 text-center">{service.content}</p>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-12">
              {service.features.map(f => (
                <div key={f} className="flex items-center gap-3">
                  <svg className="w-5 h-5 text-brand-gold shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" /></svg>
                  <span className="font-body text-sm text-brand-dark/70">{f}</span>
                </div>
              ))}
            </div>
            <div className="text-center">
              <Link href="/lien-he" className="btn-primary">Yêu Cầu Báo Giá</Link>
            </div>
          </div>
        </div>
      </Section>
    </>
  );
}
