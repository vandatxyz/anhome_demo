const API_BASE = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

async function request<T>(
  endpoint: string,
  options?: RequestInit
): Promise<T> {
  const res = await fetch(`${API_BASE}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
    next: { revalidate: 3600 },
  });
  if (!res.ok) {
    const error = await res.json().catch(() => ({ message: 'Request failed' }));
    throw new Error(error.message || `HTTP ${res.status}`);
  }
  return res.json();
}

export const api = {
  // Public
  getHome: () =>
    request<{
      banners: any[];
      featuredProjects: any[];
      featuredServices: any[];
      latestPosts: any[];
    }>('/public/home'),

  getServices: (params?: { page?: number; pageSize?: number }) => {
    const qs = params
      ? `?page=${params.page || 1}&pageSize=${params.pageSize || 12}`
      : '';
    return request<{ data: any[]; total: number }>(`/public/services${qs}`);
  },

  getService: (slug: string) => request<any>(`/public/services/${slug}`),

  getProjects: (params?: { page?: number; pageSize?: number; category?: string; style?: string }) => {
    const qs = new URLSearchParams();
    if (params?.page) qs.set('page', String(params.page));
    if (params?.pageSize) qs.set('pageSize', String(params.pageSize));
    if (params?.category) qs.set('category', params.category);
    if (params?.style) qs.set('style', params.style);
    const str = qs.toString();
    return request<{
      data: any[];
      total: number;
      page: number;
      pageSize: number;
      totalPages: number;
    }>(`/public/projects${str ? '?' + str : ''}`);
  },

  getProject: (slug: string) => request<any>(`/public/projects/${slug}`),

  getPosts: (params?: { page?: number; pageSize?: number; category?: string }) => {
    const qs = new URLSearchParams();
    if (params?.page) qs.set('page', String(params.page));
    if (params?.pageSize) qs.set('pageSize', String(params.pageSize));
    if (params?.category) qs.set('category', params.category);
    const str = qs.toString();
    return request<{
      data: any[];
      total: number;
      page: number;
      pageSize: number;
      totalPages: number;
    }>(`/public/posts${str ? '?' + str : ''}`);
  },

  getPost: (slug: string) => request<any>(`/public/posts/${slug}`),

  getCategories: (type: 'project' | 'post') =>
    request<{ data: any[] }>(`/public/categories?type=${type}`),

  submitContact: (data: {
    fullName: string;
    phone: string;
    email?: string;
    need: string;
    budget?: string;
    message: string;
  }) =>
    request<{ id: string }>('/public/contact', {
      method: 'POST',
      body: JSON.stringify(data),
    }),

  // Admin
  login: (email: string, password: string) =>
    request<{ accessToken: string; refreshToken: string; user: any }>(
      '/auth/login',
      {
        method: 'POST',
        body: JSON.stringify({ email, password }),
      }
    ),

  refreshToken: (refreshToken: string) =>
    request<{ accessToken: string }>('/auth/refresh-token', {
      method: 'POST',
      body: JSON.stringify({ refreshToken }),
    }),

  logout: (refreshToken: string) =>
    request<void>('/auth/logout', {
      method: 'POST',
      body: JSON.stringify({ refreshToken }),
    }),
};

export function getAuthHeader(accessToken: string) {
  return { Authorization: `Bearer ${accessToken}` };
}
