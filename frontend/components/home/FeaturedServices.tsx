'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';

const services = [
  { title: 'Thiết Kế Nội Thất', desc: 'Giải pháp thiết kế nội thất luxury, tối giản, phù hợp với từng không gian sống.', icon: 'M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6' },
  { title: 'Thi Công Nội Thất', desc: 'Thi công trọn gói với đội ngũ thợ lành nghề, vật liệu cao cấp, đảm bảo tiến độ.', icon: 'M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4' },
  { title: 'Tư Vấn Phong Thủy', desc: 'Tư vấn phong thủy không gian sống, kết hợp hài hòa giữa thẩm mỹ và năng lượng.', icon: 'M12 3v1m0 16v1m9-9h-1M4 12H3m15.364 6.364l-.707-.707M6.343 6.343l-.707-.707m12.728 0l-.707.707M6.343 17.657l-.707.707M16 12a4 4 0 11-8 0 4 4 0 018 0z' },
  { title: 'Thiết Kế Kiến Trúc', desc: 'Thiết kế kiến trúc biệt thự, nhà phố với phong cách hiện đại và sang trọng.', icon: 'M8 14v3m4-3v3m4-3v3M3 21h18M3 10h18M3 7l9-4 9 4M4 10h16v11H4V10z' },
];

export default function FeaturedServices() {
  return (
    <Section>
      <div className="container-custom">
        <SectionHeader subtitle="Dịch Vụ Của Chúng Tôi" title="Giải Pháp Nội Thất Toàn Diện" description="Từ thiết kế đến thi công, chúng tôi cung cấp dịch vụ trọn gói với chất lượng cao cấp nhất." />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {services.map((service, i) => (
            <motion.div
              key={service.title}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.1 }}
              className="group bg-white border border-gray-100 p-8 hover:border-brand-gold/30 hover:shadow-xl transition-all duration-300"
            >
              <div className="w-12 h-12 flex items-center justify-center mb-6 text-brand-gold">
                <svg className="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d={service.icon} />
                </svg>
              </div>
              <h3 className="font-heading text-lg mb-3">{service.title}</h3>
              <p className="font-body text-sm text-brand-dark/60 leading-relaxed mb-6">{service.desc}</p>
              <Link href="/dich-vu" className="font-body text-sm text-brand-gold tracking-wider uppercase inline-flex items-center gap-2 group-hover:gap-3 transition-all cursor-pointer">
                Chi tiết
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 8l4 4m0 0l-4 4m4-4H3" /></svg>
              </Link>
            </motion.div>
          ))}
        </div>
      </div>
    </Section>
  );
}
