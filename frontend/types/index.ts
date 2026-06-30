export interface Project {
  id: string;
  title: string;
  slug: string;
  shortDescription: string;
  content: string;
  categoryId: string;
  categoryName?: string;
  style: string;
  area: string;
  location: string;
  year: string;
  thumbnailUrl: string;
  images: ProjectImage[];
  isFeatured: boolean;
  status: string;
  seoTitle?: string;
  seoDescription?: string;
  createdAt: string;
  publishedAt?: string;
}

export interface ProjectImage {
  id: string;
  projectId: string;
  imageUrl: string;
  publicId: string;
  altText: string;
  sortOrder: number;
}

export interface Service {
  id: string;
  title: string;
  slug: string;
  shortDescription: string;
  content: string;
  icon?: string;
  imageUrl?: string;
  isFeatured: boolean;
  status: string;
  seoTitle?: string;
  seoDescription?: string;
  createdAt: string;
}

export interface Post {
  id: string;
  title: string;
  slug: string;
  shortDescription: string;
  content: string;
  coverImage?: string;
  categoryId: string;
  categoryName?: string;
  seoTitle?: string;
  seoDescription?: string;
  createdAt: string;
  publishedAt?: string;
}

export interface Category {
  id: string;
  name: string;
  slug: string;
  type: 'project' | 'post';
  description?: string;
}

export interface Contact {
  id: string;
  fullName: string;
  phone: string;
  email: string;
  need: string;
  budget?: string;
  message: string;
  status: 'new' | 'in_progress' | 'completed';
  createdAt: string;
}

export interface Banner {
  id: string;
  title: string;
  subtitle?: string;
  imageUrl: string;
  linkUrl?: string;
  sortOrder: number;
  isActive: boolean;
}

export interface SiteSetting {
  id: string;
  key: string;
  value: string;
  group: string;
}

export interface User {
  id: string;
  email: string;
  fullName: string;
  role: 'admin' | 'editor';
  createdAt: string;
}

export interface ApiResponse<T> {
  data: T;
  message?: string;
  total?: number;
  page?: number;
  pageSize?: number;
}

export interface PaginatedResponse<T> {
  data: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}
