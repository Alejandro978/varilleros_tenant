import { HttpInterceptorFn } from '@angular/common/http';

export const tenantInterceptor: HttpInterceptorFn = (req, next) => {
  const isAdmin = req.url.includes('/api/admin/');
  if (isAdmin) return next(req);
  const slug = localStorage.getItem('tenant_slug');
  return next(slug
    ? req.clone({ setHeaders: { 'X-Tenant-Id': slug } })
    : req);
};
