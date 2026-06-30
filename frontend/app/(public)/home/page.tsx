import { Metadata } from 'next';
import Hero from '@/components/home/Hero';
import AboutIntro from '@/components/home/AboutIntro';
import FeaturedServices from '@/components/home/FeaturedServices';
import FeaturedProjects from '@/components/home/FeaturedProjects';
import Process from '@/components/home/Process';
import WhyChoose from '@/components/home/WhyChoose';
import Testimonials from '@/components/home/Testimonials';
import CTA from '@/components/home/CTA';

export const metadata: Metadata = {
  title: 'An Homes - Thiết Kế & Thi Công Nội Thất Cao Cấp',
  description: 'An Homes - Đơn vị thiết kế và thi công nội thất cao cấp hàng đầu. Tạo không gian sống đẳng cấp với phong cách Luxury Modern Minimal.',
  keywords: ['thiết kế nội thất', 'thi công nội thất', 'nội thất cao cấp', 'An Homes', 'thiết kế kiến trúc', 'tư vấn nội thất', 'nội thất luxury', 'biệt thự', 'căn hộ cao cấp'],
  alternates: { canonical: '/' },
};

export default function HomePage() {
  return (
    <>
      <Hero />
      <AboutIntro />
      <FeaturedServices />
      <FeaturedProjects />
      <Process />
      <WhyChoose />
      <Testimonials />
      <CTA />
    </>
  );
}
