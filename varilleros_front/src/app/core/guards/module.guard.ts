import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

/**
 * Guard de módulos — protege rutas que requieren un módulo activo.
 *
 * Uso en app.routes.ts:
 *   canActivate: [moduleGuard],
 *   data: { module: 'CLIENTES' }
 *
 * El código del módulo se compara en minúsculas contra los módulos
 * activos del tenant (que vienen en mayúsculas desde la API).
 * Si el módulo no está activo → redirige al dashboard.
 */
export const moduleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const auth   = inject(AuthService);
  const router = inject(Router);

  const required: string = route.data['module'];
  if (!required) return true; // sin restricción de módulo

  const hasModule = auth.modulesSignal().some(
    m => m.code.toLowerCase() === required.toLowerCase()
  );

  return hasModule || router.createUrlTree(['/dashboard']);
};
