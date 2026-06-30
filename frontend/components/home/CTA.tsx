'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import Link from 'next/link';

export default function CTA() {
  return (
    <Section bg="dark">
      <div className="container-custom text-center">
        <motion.div
          initial={{ opacity: 0, y: 30 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.8 }}
        >
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">
            Bắt Đầu Ngay
          </p>
          <h2 className="font-heading text-3xl md:text-4xl lg:text-5xl text-white mb-6">
            Sẵn Sàng Kiến Tạo<br />Không Gian Của Bạn?
          </h2>
          <p className="font-body text-white/60 text-base md:text-lg max-w-2xl mx-auto mb-10 leading-relaxed">
            Liên hệ với chúng tôi ngay hôm nay để nhận tư vấn miễn phí và báo giá chi tiết cho dự án của bạn.
          </p>
          <div className="flex flex-col sm:flex-row items-center justify-center gap-4">
            <Link href="/lien-he" className="btn-primary">Đăng Ký Tư Vấn</Link>
            <a href={`tel:${process.env.NEXT_PUBLIC_PHONE || '0909xxx'}`} className="btn-white">Gọi: {process.env.NEXT_PUBLIC_PHONE || '0909.xxx.xxx'}</a>
          </div>
        </motion.div>
      </div>
    </Section>
  );
}
