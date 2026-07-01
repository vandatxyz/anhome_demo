'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { api } from '@/lib/api';

interface Project {
  id: string;
  title: string;
  slug: string;
  shortDescription: string;
  category: { name: string };
  style: string;
  area: string;
  thumbnailUrl: string;
}

export default function FeaturedProjects() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.getProjects({ page: 1, pageSize: 4 })
      .then(res => setProjects(res.data))
      .catch(() => setProjects([]))
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <Section bg="cream">
        <div className="container-custom">
          <SectionHeader subtitle="Dự Án Tiêu Biểu" title="Những Tác Phẩm Nổi Bật" description="Mỗi dự án là sự kết hợp hoàn hảo giữa thẩm mỹ và công năng, mang đến không gian sống đẳng cấp." />
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            {[1,2,3,4].map(i => (
              <div key={i} className="animate-pulse">
                <div className="aspect-[3/4] bg-neutral-200 mb-4" />
                <div className="h-4 bg-neutral-200 rounded w-3/4 mb-2" />
                <div className="h-3 bg-neutral-200 rounded w-1/2" />
              </div>
            ))}
          </div>
        </div>
      </Section>
    );
  }

  return (
    <Section bg="cream">
      <div className="container-custom">
        <SectionHeader subtitle="Dự Án Tiêu Biểu" title="Những Tác Phẩm Nổi Bật" description="Mỗi dự án là sự kết hợp hoàn hảo giữa thẩm mỹ và công năng, mang đến không gian sống đẳng cấp." />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {projects.map((project, i) => (
            <motion.div
              key={project.id}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.1 }}
            >
              <Link
                href={`/du-an/${project.slug}`}
                className="group block"
              >
                <div className="aspect-[3/4] bg-gradient-to-br from-neutral-200 to-neutral-300 mb-4 overflow-hidden relative">
                  {project.thumbnailUrl ? (
                    <img src={project.thumbnailUrl} alt={project.title} className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
                  ) : (
                    <div className="absolute inset-0 bg-brand-black/0 group-hover:bg-brand-black/40 transition-all duration-500 flex items-center justify-center">
                      <span className="text-white font-body text-sm tracking-widest uppercase opacity-0 group-hover:opacity-100 transition-opacity duration-300">Xem chi tiết</span>
                    </div>
                  )}
                </div>
                <h3 className="font-heading text-base mb-1 group-hover:text-brand-gold transition-colors">{project.title}</h3>
                <p className="font-body text-xs text-brand-dark/50 tracking-wider uppercase">{project.category?.name || ''} · {project.area} · {project.style}</p>
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
