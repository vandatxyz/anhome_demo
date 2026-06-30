'use client';
import { motion } from 'framer-motion';
import Link from 'next/link';

export default function Hero() {
  return (
    <section className="relative h-screen min-h-[700px] flex items-center justify-center overflow-hidden">
      <div className="absolute inset-0 bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/40 via-black/20 to-black/60 z-10" />
        <div className="w-full h-full bg-gradient-to-br from-brand-black via-neutral-800 to-neutral-900" />
      </div>
      <div className="relative z-20 container-custom text-center text-white">
        <motion.p initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.8, delay: 0.2 }} className="font-body text-brand-gold text-sm md:text-base tracking-[0.4em] uppercase mb-6">
          Thiết Kế & Thi Công Nội Thất Cao Cấp
        </motion.p>
        <motion.h1 initial={{ opacity: 0, y: 30 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.8, delay: 0.4 }} className="font-heading text-4xl md:text-5xl lg:text-7xl font-semibold tracking-wide leading-tight mb-8">
          Kiến Tạo Không Gian<br />Đẳng Cấp Của Bạn
        </motion.h1>
        <motion.p initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.8, delay: 0.6 }} className="font-body text-white/70 text-base md:text-lg max-w-2xl mx-auto mb-12 leading-relaxed">
          An Homes mang đến giải pháp thiết kế và thi công nội thất luxury, tối giản nhưng đầy tinh tế, phù hợp với từng không gian sống hiện đại.
        </motion.p>
        <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.8, delay: 0.8 }} className="flex flex-col sm:flex-row items-center justify-center gap-4">
          <Link href="/lien-he" className="btn-primary">Tư Vấn Miễn Phí</Link>
          <Link href="/du-an" className="btn-white">Xem Dự Án</Link>
        </motion.div>
      </div>
      <motion.div initial={{ opacity: 0 }} animate={{ opacity: 1 }} transition={{ delay: 1.2 }} className="absolute bottom-10 left-1/2 -translate-x-1/2 z-20">
        <div className="w-6 h-10 border-2 border-white/40 rounded-full flex justify-center">
          <motion.div animate={{ y: [0, 12, 0] }} transition={{ duration: 1.5, repeat: Infinity }} className="w-1 h-2 bg-white/60 rounded-full mt-2" />
        </div>
      </motion.div>
    </section>
  );
}
