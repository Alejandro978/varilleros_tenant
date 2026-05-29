import { HttpInterceptorFn } from '@angular/common/http';

export const tenantInterceptor: HttpInterceptorFn = (req, next) => {
  const skipTenant = req.url.includes('/api/admin/') || req.url.includes('/api/auth/');
  if (skipTenant) return next(req);
  const slug = localStorage.getItem('tenant_slug');
  return next(slug
    ? req.clone({ setHeaders: { 'X-Tenant-Id': slug } })
    : req);
};
