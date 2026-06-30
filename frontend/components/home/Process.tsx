'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';

const steps = [
  { num: '01', title: 'Tư Vấn & Tiếp Nhận', desc: 'Lắng nghe nhu cầu, mong muốn và ngân sách của khách hàng. Tiếp nhận thông tin phong thủy, diện tích và yêu cầu đặc biệt.' },
  { num: '02', title: 'Thiết Kế Concept', desc: 'Đội ngũ kiến trúc sư triển khai concept thiết kế 2D/3D, lựa chọn vật liệu, màu sắc và phong cách phù hợp.' },
  { num: '03', title: 'Báo Giá & Ký Hợp Đồng', desc: 'Báo giá chi tiết minh bạch, thống nhất tiến độ và ký hợp đồng rõ ràng các điều khoản cam kết.' },
  { num: '04', title: 'Thi Công & Giám Sát', desc: 'Thi công với đội ngũ thợ lành nghề, giám sát chặt chẽ từng hạng mục để đảm bảo chất lượng.' },
  { num: '05', title: 'Bàn Giao & Bảo Hành', desc: 'Bàn giao hoàn thiện, hướng dẫn sử dụng và bảo hành chính hãng theo cam kết.' },
];

export default function Process() {
  return (
    <Section>
      <div className="container-custom">
        <SectionHeader subtitle="Quy Trình Làm Việc" title="Hành Trình Từ Ý Tưởng Đến Hiện Thực" description="Quy trình chuyên nghiệp, minh bạch từ khâu tư vấn đến bàn giao." />
        <div className="relative">
          <div className="hidden lg:block absolute top-1/2 left-0 right-0 h-px bg-brand-gold/20 -translate-y-1/2" />
          <div className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-5 gap-8">
            {steps.map((step, i) => (
              <motion.div
                key={step.num}
                initial={{ opacity: 0, y: 30 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: i * 0.15 }}
                className="relative text-center"
              >
                <div className="w-12 h-12 rounded-full bg-brand-gold text-white font-heading text-sm flex items-center justify-center mx-auto mb-6 relative z-10">
                  {step.num}
                </div>
                <h3 className="font-heading text-base mb-3">{step.title}</h3>
                <p className="font-body text-sm text-brand-dark/60 leading-relaxed">{step.desc}</p>
              </motion.div>
            ))}
          </div>
        </div>
      </div>
    </Section>
  );
}
