import { Metadata } from 'next';
import Section from '@/components/shared/Section';
import Link from 'next/link';

type Props = { params: Promise<{ slug: string }> };

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const { slug } = await params;
  return {
    title: `Dự Án - ${decodeURIComponent(slug).replace(/-/g, ' ')} | An Homes`,
    description: 'Chi tiết dự án thiết kế và thi công nội thất của An Homes.',
  };
}

const projects: Record<string, { title: string; category: string; area: string; style: string; location: string; year: string; content: string }> = {
  'penthouse-the-landmark': {
    title: 'Penthouse The Landmark',
    category: 'Căn hộ cao cấp',
    area: '250m²',
    style: 'Luxury Modern',
    location: 'Quận 1, TP.HCM',
    year: '2024',
    content: 'Dự án penthouse tại The Landmark với phong cách Luxury Modern, tối giản nhưng đầy tinh tế.',
  },
};

export default async function ProjectDetailPage({ params }: Props) {
  const { slug } = await params;
  const project = projects[slug] || projects['penthouse-the-landmark'];

  return (
    <>
      <section className="relative h-[60vh] min-h-[500px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">{project.category}</p>
          <h1 className="font-heading text-3xl md:text-4xl lg:text-5xl mb-4">{project.title}</h1>
          <div className="flex flex-wrap gap-4 font-body text-sm text-white/60">
            <span>{project.area}</span><span>·</span><span>{project.style}</span><span>·</span><span>{project.location}</span><span>·</span><span>{project.year}</span>
          </div>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-12">
            <div className="lg:col-span-2">
              <div className="aspect-video bg-gradient-to-br from-neutral-200 to-neutral-300 mb-8" />
              <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed mb-6">{project.content}</p>
              <div className="grid grid-cols-2 gap-4 mt-8">
                {[1,2,3,4].map(i => <div key={i} className="aspect-video bg-gradient-to-br from-neutral-200 to-neutral-300" />)}
              </div>
            </div>
            <div>
              <div className="bg-brand-cream p-6 sticky top-24">
                <h3 className="font-heading text-lg mb-4">Thông Tin Dự Án</h3>
                <div className="space-y-3">
                  <div className="flex justify-between py-2 border-b border-gray-200"><span className="font-body text-sm text-brand-dark/60">Loại hình</span><span className="font-body text-sm">{project.category}</span></div>
                  <div className="flex justify-between py-2 border-b border-gray-200"><span className="font-body text-sm text-brand-dark/60">Diện tích</span><span className="font-body text-sm">{project.area}</span></div>
                  <div className="flex justify-between py-2 border-b border-gray-200"><span className="font-body text-sm text-brand-dark/60">Phong cách</span><span className="font-body text-sm">{project.style}</span></div>
                  <div className="flex justify-between py-2 border-b border-gray-200"><span className="font-body text-sm text-brand-dark/60">Vị trí</span><span className="font-body text-sm">{project.location}</span></div>
                  <div className="flex justify-between py-2 border-b border-gray-200"><span className="font-body text-sm text-brand-dark/60">Năm</span><span className="font-body text-sm">{project.year}</span></div>
                </div>
                <Link href="/lien-he" className="btn-primary w-full mt-6 text-center block">Tư Vấn Tương Tự</Link>
              </div>
            </div>
          </div>
        </div>
      </Section>
    </>
  );
}
