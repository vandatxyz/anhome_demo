import { ReactNode } from 'react';

interface SectionProps {
  children: ReactNode;
  className?: string;
  id?: string;
  bg?: 'white' | 'cream' | 'dark';
}

export default function Section({ children, className = '', id, bg = 'white' }: SectionProps) {
  const bgClass = { white: 'bg-white', cream: 'bg-brand-cream', dark: 'bg-brand-black text-white' }[bg];
  return (
    <section
      id={id}
      className={`section-padding ${bgClass} ${className}`}
    >
      {children}
    </section>
  );
}

export function SectionHeader({ subtitle, title, description, align = 'center' }: { subtitle?: string; title: string; description?: string; align?: 'left' | 'center' }) {
  return (
    <div className={`max-w-3xl ${align === 'center' ? 'mx-auto text-center' : ''} mb-16`}>
      {subtitle && <p className="section-subtitle">{subtitle}</p>}
      <h2 className="section-title">{title}</h2>
      <div className={`gold-line ${align === 'left' ? 'mx-0' : ''} mt-6 mb-6`} />
      {description && <p className="font-body text-brand-dark/70 text-base md:text-lg leading-relaxed">{description}</p>}
    </div>
  );
}
