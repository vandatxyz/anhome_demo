'use client';
import { motion } from 'framer-motion';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { api } from '@/lib/api';

interface Service {
  id: string;
  title: string;
  slug: string;
  shortDescription: string;
  icon: string;
  isFeatured: boolean;
}

export default function FeaturedServices() {
  const [services, setServices] = useState<Service[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.getServices({ page: 1, pageSize: 4 })
      .then(res => setServices(res.data))
      .catch(() => setServices([]))
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <Section>
        <div className="container-custom">
          <SectionHeader subtitle="Dịch Vụ Của Chúng Tôi" title="Giải Pháp Nội Thất Toàn Diện" description="Từ thiết kế đến thi công, chúng tôi cung cấp dịch vụ trọn gói với chất lượng cao cấp nhất." />
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
            {[1,2,3,4].map(i => (
              <div key={i} className="animate-pulse bg-white border border-gray-100 p-8">
                <div className="w-12 h-12 bg-neutral-200 rounded mb-6" />
                <div className="h-4 bg-neutral-200 rounded w-3/4 mb-3" />
                <div className="h-3 bg-neutral-200 rounded w-full mb-2" />
                <div className="h-3 bg-neutral-200 rounded w-2/3" />
              </div>
            ))}
          </div>
        </div>
      </Section>
    );
  }

  return (
    <Section>
      <div className="container-custom">
        <SectionHeader subtitle="Dịch Vụ Của Chúng Tôi" title="Giải Pháp Nội Thất Toàn Diện" description="Từ thiết kế đến thi công, chúng tôi cung cấp dịch vụ trọn gói với chất lượng cao cấp nhất." />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {services.map((service, i) => (
            <motion.div
              key={service.id}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.1 }}
              className="group bg-white border border-gray-100 p-8 hover:border-brand-gold/30 hover:shadow-xl transition-all duration-300"
            >
              <div className="w-12 h-12 flex items-center justify-center mb-6 text-brand-gold">
                <span className="text-2xl">{service.icon || '◆'}</span>
              </div>
              <h3 className="font-heading text-lg mb-3">{service.title}</h3>
              <p className="font-body text-sm text-brand-dark/60 leading-relaxed mb-6">{service.shortDescription}</p>
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
