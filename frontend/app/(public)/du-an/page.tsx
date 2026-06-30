import { Metadata } from 'next';
import Section from '@/components/shared/Section';
import { SectionHeader } from '@/components/shared/Section';
import Link from 'next/link';

export const metadata: Metadata = {
  title: 'Dự Án - An Homes',
  description: 'Danh sách dự án thiết kế và thi công nội thất của An Homes. Khám phá các tác phẩm nội thất cao cấp.',
  keywords: ['dự án nội thất', 'portfolio nội thất', 'thiết kế nội thất', 'thi công nội thất'],
  alternates: { canonical: '/du-an' },
};

const categories = ['Tất cả', 'Căn hộ cao cấp', 'Biệt thự', 'Nhà phố', 'Văn phòng'];

const projects = [
  { title: 'Penthouse The Landmark', category: 'Căn hộ cao cấp', area: '250m²', style: 'Luxury Modern', location: 'Quận 1, TP.HCM' },
  { title: 'Biệt Thự Phú Mỹ Hưng', category: 'Biệt thự', area: '500m²', style: 'Classic Luxury', location: 'Quận 7, TP.HCM' },
  { title: 'Nhà Phố Thảo Điền', category: 'Nhà phố', area: '180m²', style: 'Minimalist', location: 'Quận 2, TP.HCM' },
  { title: 'Căn Hộ Landmark 81', category: 'Căn hộ cao cấp', area: '200m²', style: 'Contemporary', location: 'Quận Bình Thạnh' },
  { title: 'Biệt Thự Thủ Thiêm', category: 'Biệt thự', area: '400m²', style: 'Modern Classic', location: 'Quận 2, TP.HCM' },
  { title: 'Nhà Phố Quận 3', category: 'Nhà phố', area: '120m²', style: 'Minimalist', location: 'Quận 3, TP.HCM' },
  { title: 'Văn Phòng Nguyễn Văn Trỗi', category: 'Văn phòng', area: '300m²', style: 'Modern', location: 'Quận Phú Nhuận' },
  { title: 'Penthouse The Sun Avenue', category: 'Căn hộ cao cấp', area: '220m²', style: 'Luxury Modern', location: 'Quận 2, TP.HCM' },
];

interface ProjectsPageProps {
  searchParams: Promise<{ category?: string }>;
}

export default async function ProjectsPage({ searchParams }: ProjectsPageProps) {
  const params = await searchParams;
  const activeCategory = params.category || 'Tất cả';

  const filteredProjects = activeCategory === 'Tất cả'
    ? projects
    : projects.filter(p => p.category === activeCategory);

  return (
    <>
      <section className="relative h-[50vh] min-h-[400px] flex items-center justify-center bg-brand-black">
        <div className="absolute inset-0 bg-gradient-to-b from-black/50 to-black/70" />
        <div className="relative z-10 container-custom text-center text-white">
          <p className="font-body text-brand-gold text-sm tracking-[0.3em] uppercase mb-4">Portfolio</p>
          <h1 className="font-heading text-4xl md:text-5xl lg:text-6xl">Dự Án Của Chúng Tôi</h1>
        </div>
      </section>
      <Section>
        <div className="container-custom">
          <SectionHeader subtitle="Dự Án Tiêu Biểu" title="Những Tác Phẩm Nổi Bật" description="Mỗi dự án là sự kết hợp hoàn hảo giữa thẩm mỹ và công năng." />
          <div className="flex flex-wrap gap-3 mb-12">
            {categories.map((cat) => {
              const isActive = activeCategory === cat;
              const href = cat === 'Tất cả' ? '/du-an' : `/du-an?category=${encodeURIComponent(cat)}`;
              return (
                <Link
                  key={cat}
                  href={href}
                  className={`px-6 py-2 font-body text-sm tracking-wider border transition-colors ${
                    isActive
                      ? 'border-brand-gold bg-brand-gold text-white'
                      : 'border-brand-black/10 hover:border-brand-gold hover:text-brand-gold'
                  }`}
                >
                  {cat}
                </Link>
              );
            })}
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            {filteredProjects.map((project) => (
              <Link key={project.title} href={`/du-an/${project.title.toLowerCase().replace(/\s+/g, '-')}`} className="group cursor-pointer">
                <div className="aspect-[3/4] bg-gradient-to-br from-neutral-200 to-neutral-300 mb-4 overflow-hidden relative">
                  <div className="absolute inset-0 bg-brand-black/0 group-hover:bg-brand-black/40 transition-all duration-500 flex items-center justify-center">
                    <span className="text-white font-body text-sm tracking-widest uppercase opacity-0 group-hover:opacity-100 transition-opacity duration-300">Xem chi tiết</span>
                  </div>
                </div>
                <h3 className="font-heading text-base mb-1 group-hover:text-brand-gold transition-colors">{project.title}</h3>
                <p className="font-body text-xs text-brand-dark/50 tracking-wider uppercase">{project.category} · {project.area} · {project.style}</p>
              </Link>
            ))}
          </div>
          {filteredProjects.length === 0 && (
            <p className="text-center font-body text-brand-dark/50 py-12">Không tìm thấy dự án trong danh mục này.</p>
          )}
        </div>
      </Section>
    </>
  );
}
