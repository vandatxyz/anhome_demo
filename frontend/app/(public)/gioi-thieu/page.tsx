'use client';

import { Metadata } from 'next';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import { motion } from 'framer-motion';

const stats = [
  { number: '10+', label: 'Năm kinh nghiệm' },
  { number: '500+', label: 'Dự án hoàn thành' },
  { number: '300+', label: 'Khách hàng hài lòng' },
  { number: '50+', label: 'Kiến trúc sư' },
];

const values = [
  { title: 'Sáng Tạo', desc: 'Luôn đổi mới, không ngừng học hỏi để mang đến những giải pháp thiết kế độc đáo và tiên tiến.' },
  { title: 'Chất Lượng', desc: 'Cam kết chất lượng từng chi tiết, sử dụng vật liệu cao cấp và quy trình kiểm soát nghiêm ngặt.' },
  { title: 'Uy Tín', desc: 'Minh bạch trong mọi giao dịch, đúng cam kết về tiến độ và giá cả đã thống nhất.' },
  { title: 'Tận Tâm', desc: 'Lắng nghe và thấu hiểu từng khách hàng, đồng hành từ khâu tư vấn đến bàn giao.' },
];

export const metadata: Metadata = {
  title: 'Giới Thiệu - An Homes',
  description: 'Tìm hiểu về An Homes - đơn vị thiết kế và thi công nội thất cao cấp hàng đầu với hơn 10 năm kinh nghiệm.',
  keywords: ['giới thiệu An Homes', 'lịch sử An Homes', 'đội ngũ kiến trúc sư', 'nội thất cao cấp'],
  alternates: { canonical: '/gioi-thieu' },
};

export default function AboutPage() {
  return (
    <>
      <section className="relative h-[60vh] min-h-[500px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <motion.p initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Về Chúng Tôi</motion.p>
          <motion.h1 initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: 0.1 }} className="font-heading text-4xl md:text-5xl lg:text-6xl">Câu Chuyện An Homes</motion.h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-16 items-center">
            <div>
              <p className="section-subtitle">Về Chúng Tôi</p>
              <h2 className="section-title text-left">Kiến Tạo Không Gian Sống Đẳng Cấp</h2>
              <div className="gold-line-left mt-6 mb-8" />
              <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-6">
                An Homes được thành lập với sứ mệnh mang đến giải pháp thiết kế và thi công nội thất cao cấp, kết hợp giữa phong cách Luxury Modern Minimal và sự tinh tế của nét văn hóa Việt.
              </p>
              <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-8">
                Với đội ngũ hơn 50 kiến trúc sư và thợ lành nghề, chúng tôi đã hoàn thành hơn 500 dự án trên toàn quốc, từ căn hộ cao cấp, biệt thự đến nhà phố và văn phòng.
              </p>
              <a href="/lien-he" className="btn-primary">Liên Hệ Tư Vấn</a>
            </div>
            <div className="grid grid-cols-2 gap-4">
              {stats.map((stat, i) => (
                <motion.div key={stat.label} initial={{ opacity: 0, y: 20 }} whileInView={{ opacity: 1, y: 0 }} viewport={{ once: true }} transition={{ delay: i * 0.1 }} className="bg-brand-cream p-8 text-center">
                  <span className="font-heading text-3xl md:text-4xl text-brand-gold">{stat.number}</span>
                  <p className="font-body text-sm text-brand-dark/60 mt-2">{stat.label}</p>
                </motion.div>
              ))}
            </div>
          </div>
        </div>
      </Section>
      <Section bg="cream">
        <div className="container-custom">
          <SectionHeader subtitle="Giá Trị Cốt Lõi" title="Điều Gì Làm Nên An Homes" />
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
            {values.map((v, i) => (
              <motion.div key={v.title} initial={{ opacity: 0, y: 20 }} whileInView={{ opacity: 1, y: 0 }} viewport={{ once: true }} transition={{ delay: i * 0.1 }} className="text-center">
                <div className="w-16 h-16 rounded-full bg-brand-gold/10 flex items-center justify-center mx-auto mb-6">
                  <span className="font-heading text-brand-gold text-xl">{String(i + 1).padStart(2, '0')}</span>
                </div>
                <h3 className="font-heading text-lg mb-3">{v.title}</h3>
                <p className="font-body text-sm text-brand-dark/60 leading-relaxed">{v.desc}</p>
              </motion.div>
            ))}
          </div>
        </div>
      </Section>
    </>
  );
}
