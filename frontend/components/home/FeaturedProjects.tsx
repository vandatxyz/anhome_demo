'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';

const projects = [
  { title: 'Penthouse The Landmark', category: 'Căn hộ cao cấp', area: '250m²', style: 'Luxury Modern' },
  { title: 'Biệt Thự Phú Mỹ Hưng', category: 'Biệt thự', area: '500m²', style: 'Classic Luxury' },
  { title: 'Nhà Phố Thảo Điền', category: 'Nhà phố', area: '180m²', style: 'Minimalist' },
  { title: 'Căn Hộ Landmark 81', category: 'Căn hộ cao cấp', area: '200m²', style: 'Contemporary' },
];

export default function FeaturedProjects() {
  return (
    <Section bg="cream">
      <div className="container-custom">
        <SectionHeader subtitle="Dự Án Tiêu Biểu" title="Những Tác Phẩm Nổi Bật" description="Mỗi dự án là sự kết hợp hoàn hảo giữa thẩm mỹ và công năng, mang đến không gian sống đẳng cấp." />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {projects.map((project, i) => (
            <motion.div
              key={project.title}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.1 }}
            >
              <Link
                href={`/du-an/${encodeURIComponent(project.title.toLowerCase().replace(/\s+/g, '-'))}`}
                className="group block"
              >
                <div className="aspect-[3/4] bg-gradient-to-br from-neutral-200 to-neutral-300 mb-4 overflow-hidden relative">
                  <div className="absolute inset-0 bg-brand-black/0 group-hover:bg-brand-black/40 transition-all duration-500 flex items-center justify-center">
                    <span className="text-white font-body text-sm tracking-widest uppercase opacity-0 group-hover:opacity-100 transition-opacity duration-300">Xem chi tiết</span>
                  </div>
                </div>
                <h3 className="font-heading text-base mb-1 group-hover:text-brand-gold transition-colors">{project.title}</h3>
                <p className="font-body text-xs text-brand-dark/50 tracking-wider uppercase">{project.category} · {project.area} · {project.style}</p>
              </Link>
            </motion.div>
          ))}
        </div>
        <div className="text-center mt-12">
          <Link href="/du-an" className="btn-outline">Xem Tất Cả Dự Án</Link>
        </div>
      </div>
    </Section>
  );
}
