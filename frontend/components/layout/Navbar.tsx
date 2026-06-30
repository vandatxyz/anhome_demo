'use client';

import { useState, useEffect } from 'react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { motion, AnimatePresence } from 'framer-motion';

const navLinks = [
  { href: '/gioi-thieu', label: 'Giới thiệu' },
  { href: '/dich-vu', label: 'Dịch vụ' },
  { href: '/du-an', label: 'Dự án' },
  { href: '/tin-tuc', label: 'Tin tức' },
  { href: '/lien-he', label: 'Liên hệ' },
];

export default function Navbar() {
  const [scrolled, setScrolled] = useState(false);
  const [mobileOpen, setMobileOpen] = useState(false);
  const pathname = usePathname();

  useEffect(() => {
    const handleScroll = () => setScrolled(window.scrollY > 50);
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  useEffect(() => { setMobileOpen(false); }, [pathname]);

  return (
    <header className={`fixed top-0 left-0 right-0 z-50 transition-all duration-300 ${scrolled ? 'bg-white/95 backdrop-blur-sm shadow-sm' : 'bg-transparent'}`}>
      <nav className="container-custom flex items-center justify-between h-20 lg:h-24">
        <Link href="/" className="flex items-center gap-2">
          <span className="font-heading text-xl lg:text-2xl font-semibold tracking-wider text-brand-black">AN HOMES</span>
        </Link>
        <div className="hidden lg:flex items-center gap-8">
          {navLinks.map((link) => (
            <Link key={link.href} href={link.href} className={`font-body text-sm tracking-widest uppercase transition-colors duration-200 cursor-pointer ${pathname === link.href ? 'text-brand-gold' : scrolled ? 'text-brand-dark hover:text-brand-gold' : 'text-white/80 hover:text-brand-gold'}`}>
              {link.label}
            </Link>
          ))}
          <a href={`tel:${process.env.NEXT_PUBLIC_PHONE || '0909xxx'}`} className="btn-primary text-xs py-3 px-6">Gọi ngay</a>
        </div>
        <button onClick={() => setMobileOpen(!mobileOpen)} className="lg:hidden flex flex-col gap-1.5 p-2 cursor-pointer" aria-label="Toggle menu">
          <motion.span animate={mobileOpen ? { rotate: 45, y: 6 } : { rotate: 0, y: 0 }} className="block w-6 h-0.5 bg-current" />
          <motion.span animate={mobileOpen ? { opacity: 0 } : { opacity: 1 }} className="block w-6 h-0.5 bg-current" />
          <motion.span animate={mobileOpen ? { rotate: -45, y: -6 } : { rotate: 0, y: 0 }} className="block w-6 h-0.5 bg-current" />
        </button>
      </nav>
      <AnimatePresence>
        {mobileOpen && (
          <motion.div initial={{ opacity: 0, height: 0 }} animate={{ opacity: 1, height: 'auto' }} exit={{ opacity: 0, height: 0 }} className="lg:hidden bg-white border-t">
            <div className="container-custom py-6 flex flex-col gap-4">
              {navLinks.map((link) => (
                <Link key={link.href} href={link.href} className={`font-body text-sm tracking-widest uppercase py-2 cursor-pointer ${pathname === link.href ? 'text-brand-gold' : 'text-brand-dark'}`}>
                  {link.label}
                </Link>
              ))}
            </div>
          </motion.div>
        )}
      </AnimatePresence>
    </header>
  );
}
