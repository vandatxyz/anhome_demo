'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';

const reasons = [
  { title: 'Thiết Kế Độc Quyền', desc: 'Mỗi dự án là một tác phẩm riêng, được thiết kế riêng theo nhu cầu và phong cách của gia chủ.' },
  { title: 'Vật Liệu Cao Cấp', desc: 'Sử dụng vật liệu nhập khẩu chính hãng, đảm bảo độ bền và thẩm mỹ lâu dài.' },
  { title: 'Đội Ngũ Chuyên Nghiệp', desc: 'Kiến trúc sư và thợ lành nghề với hơn 10 năm kinh nghiệm trong ngành nội thất cao cấp.' },
  { title: 'Bảo Hành Dài Hạn', desc: 'Cam kết bảo hành 2-5 năm tùy hạng mục, hỗ trợ bảo trì trọn đời.' },
  { title: 'Tiến Độ Đảm Bảo', desc: 'Ký hợp đồng rõ ràng, cam kết tiến độ bàn giao đúng hạn hoặc bồi thường.' },
  { title: 'Giá Cả Minh Bạch', desc: 'Báo giá chi tiết từng hạng mục, không phát sinh chi phí ngoài hợp đồng.' },
];

export default function WhyChoose() {
  return (
    <Section bg="cream">
      <div className="container-custom">
        <SectionHeader subtitle="Tại Sao Chọn An Homes" title="Đối Tác Tin Cậy Của Bạn" description="Chúng tôi không chỉ thiết kế nội thất, chúng tôi kiến tạo trải nghiệm sống đẳng cấp." />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {reasons.map((reason, i) => (
            <motion.div
              key={reason.title}
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.1 }}
              className="flex gap-4"
            >
              <div className="w-8 h-8 rounded-full bg-brand-gold/10 flex items-center justify-center shrink-0 mt-1">
                <svg className="w-4 h-4 text-brand-gold" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                </svg>
              </div>
              <div>
                <h3 className="font-heading text-base mb-2">{reason.title}</h3>
                <p className="font-body text-sm text-brand-dark/60 leading-relaxed">{reason.desc}</p>
              </div>
            </motion.div>
          ))}
        </div>
      </div>
    </Section>
  );
}
