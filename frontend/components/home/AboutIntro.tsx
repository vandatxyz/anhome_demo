'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';

const stats = [
  { number: '10+', label: 'Năm kinh nghiệm' },
  { number: '500+', label: 'Dự án hoàn thành' },
  { number: '300+', label: 'Khách hàng hài lòng' },
  { number: '50+', label: 'Kiến trúc sư' },
];

export default function AboutIntro() {
  return (
    <Section bg="cream">
      <div className="container-custom">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-16 items-center">
          <div>
            <SectionHeader subtitle="Về Chúng Tôi" title="Kiến Tạo Không Gian Sống Đẳng Cấp" align="left" />
            <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-6">
              An Homes là đơn vị thiết kế và thi công nội thất cao cấp hàng đầu, với hơn 10 năm kinh nghiệm trong ngành. Chúng tôi mang đến giải pháp không gian sống đẳng cấp, kết hợp giữa phong cách Luxury và tối giản hiện đại.
            </p>
            <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-8">
              Mỗi dự án là một tác phẩm nghệ thuật, được chăm chút từng chi tiết để tôn vinh phong cách sống riêng của mỗi gia chủ.
            </p>
            <a href="/gioi-thieu" className="btn-outline">Tìm Hiểu Thêm</a>
          </div>
          <div className="grid grid-cols-2 gap-4">
            {stats.map((stat, i) => (
              <motion.div
                key={stat.label}
                initial={{ opacity: 0, y: 20 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: i * 0.1 }}
                className="bg-white p-8 text-center"
              >
                <span className="font-heading text-3xl md:text-4xl text-brand-gold">{stat.number}</span>
                <p className="font-body text-sm text-brand-dark/60 mt-2">{stat.label}</p>
              </motion.div>
            ))}
          </div>
        </div>
      </div>
    </Section>
  );
}
