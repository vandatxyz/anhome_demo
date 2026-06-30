'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';

const testimonials = [
  { name: 'Chị Nguyễn Thị Lan', role: 'Chủ nhà phố Quận 7', content: 'An Homes đã biến ngôi nhà của tôi thành một tác phẩm nghệ thuật. Thiết kế tối giản nhưng vô cùng tinh tế, đúng như mong đợi của gia đình.' },
  { name: 'Anh Trần Văn Minh', role: 'Chủ penthouse The Landmark', content: 'Đội ngũ An Homes rất chuyên nghiệp, từ khâu thiết kế đến thi công đều rất chi tiết. Tiến độ đúng hẹn, chất lượng vượt mong đợi.' },
  { name: 'Chị Phạm Thu Hà', role: 'Chủ biệt thự Phú Mỹ Hưng', content: 'Tôi đã tham khảo nhiều đơn vị nhưng chỉ An Homes mang đến giải pháp phù hợp nhất với phong cách sống của gia đình tôi.' },
];

export default function Testimonials() {
  return (
    <Section>
      <div className="container-custom">
        <SectionHeader subtitle="Phản Hồi Khách Hàng" title="Được Tin Tưởng Bởi Hàng Trăm Gia Đình" />
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          {testimonials.map((t, i) => (
            <motion.div
              key={t.name}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.15 }}
              className="bg-brand-cream p-8 relative"
            >
              <svg className="w-8 h-8 text-brand-gold/30 mb-4" fill="currentColor" viewBox="0 0 24 24">
                <path d="M14.017 21v-7.391c0-5.704 3.731-9.57 8.983-10.609l.995 2.151c-2.432.917-3.995 3.638-3.995 5.849h4v10h-9.983zm-14.017 0v-7.391c0-5.704 3.748-9.57 9-10.609l.996 2.151c-2.433.917-3.996 3.638-3.996 5.849h3.983v10h-9.983z" />
              </svg>
              <p className="font-body text-brand-dark/70 leading-relaxed mb-6">{t.content}</p>
              <div>
                <p className="font-heading text-sm">{t.name}</p>
                <p className="font-body text-xs text-brand-dark/50 mt-1">{t.role}</p>
              </div>
            </motion.div>
          ))}
        </div>
      </div>
    </Section>
  );
}
