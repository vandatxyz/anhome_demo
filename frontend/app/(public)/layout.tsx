import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import Navbar from '@/components/layout/Navbar';
import Footer from '@/components/layout/Footer';
import './globals.css';

const inter = Inter({ subsets: ['latin'], variable: '--font-body' });

export const metadata: Metadata = {
  metadataBase: new URL(process.env.NEXT_PUBLIC_SITE_URL || 'https://noithatanhomes.vn'),
  title: {
    default: 'An Homes - Thiết Kế & Thi Công Nội Thất Cao Cấp',
    template: '%s | An Homes',
  },
  description: 'An Homes - Đơn vị thiết kế và thi công nội thất cao cấp hàng đầu. Tạo không gian sống đẳng cấp với phong cách Luxury Modern Minimal.',
  keywords: ['thiết kế nội thất', 'thi công nội thất', 'nội thất cao cấp', 'An Homes', 'thiết kế kiến trúc', 'tư vấn nội thất', 'nội thất luxury'],
  openGraph: {
    type: 'website',
    locale: 'vi_VN',
    url: 'https://noithatanhomes.vn',
    siteName: 'An Homes',
    title: 'An Homes - Thiết Kế & Thi Công Nội Thất Cao Cấp',
    description: 'Đơn vị thiết kế và thi công nội thất cao cấp hàng đầu. Tạo không gian sống đẳng cấp với phong cách Luxury Modern Minimal.',
    images: [{ url: '/images/og-default.jpg', width: 1200, height: 630 }],
  },
  robots: { index: true, follow: true },
  alternates: { canonical: '/' },
  other: {
    'script:ld+json': JSON.stringify({
      '@context': 'https://schema.org',
      '@type': 'Organization',
      name: 'An Homes',
      description: 'Đơn vị thiết kế và thi công nội thất cao cấp hàng đầu',
      url: 'https://noithatanhomes.vn',
      logo: 'https://noithatanhomes.vn/images/logo.png',
      address: {
        '@type': 'PostalAddress',
        addressLocality: 'Hồ Chí Minh',
        addressCountry: 'VN',
      },
      contactPoint: {
        '@type': 'ContactPoint',
        telephone: process.env.NEXT_PUBLIC_PHONE || '0909.xxx.xxx',
        contactType: 'customer service',
        availableLanguage: ['Vietnamese'],
      },
      sameAs: [],
    }),
  },
};

export default function PublicLayout({ children }: { children: React.ReactNode }) {
  return (
    <body className={`${inter.variable} font-sans`}>
      <Navbar />
      <main>{children}</main>
      <Footer />
    </body>
  );
}
