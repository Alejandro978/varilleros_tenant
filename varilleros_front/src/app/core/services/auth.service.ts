import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap, map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ActiveModuleDto } from '../models/module.model';

interface LoginResponse {
  accessToken: string;
  tenantName: string;
  slug: string;
  tenantId: number;
  modules: ActiveModuleDto[];
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http   = inject(HttpClient);
  private readonly router = inject(Router);

  // Signal reactivo — sidebar y cualquier componente se recalculan automáticamente
  readonly modulesSignal = signal<ActiveModuleDto[]>(this._loadModulesFromStorage());

  login(slug: string, password: string): Observable<void> {
    return this.http
      .post<LoginResponse>(`${environment.apiUrl}/api/auth/login`, { slug, password })
      .pipe(
        tap(res => {
          localStorage.setItem('access_token',   res.accessToken);
          localStorage.setItem('tenant_slug',    res.slug);
          localStorage.setItem('tenant_name',    res.tenantName);
          localStorage.setItem('tenant_id',      String(res.tenantId));
          localStorage.setItem('tenant_modules', JSON.stringify(res.modules));
          this.modulesSignal.set(res.modules);
        }),
        map(() => void 0)
      );
  }

  /**
   * Refresca los módulos activos desde el backend sin re-login.
   * Llama a GET /api/auth/modules/{tenantId} que aplica el doble filtro:
   *   tenant_module.is_active AND modules_catalog.is_active (interruptor maestro).
   * Se invoca en cada navegación de ruta desde MainLayoutComponent.
   */
  refreshModules(): Observable<void> {
    const tenantId = Number(localStorage.getItem('tenant_id'));
    if (!tenantId) return of(void 0);

    return this.http
      .get<ActiveModuleDto[]>(`${environment.apiUrl}/api/auth/modules/${tenantId}`)
      .pipe(
        tap(modules => {
          this.modulesSignal.set(modules);
          localStorage.setItem('tenant_modules', JSON.stringify(modules));
        }),
        map(() => void 0),
        catchError(() => of(void 0))   // nunca bloquea la navegación si falla
      );
  }

  logout(): void {
    localStorage.clear();
    this.modulesSignal.set([]);
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

  getTenantName(): string {
    return localStorage.getItem('tenant_name') ?? '';
  }

  getModules(): ActiveModuleDto[] {
    return this.modulesSignal();
  }

  private _loadModulesFromStorage(): ActiveModuleDto[] {
    const raw = localStorage.getItem('tenant_modules');
    if (!raw) return [];
    try { return JSON.parse(raw) as ActiveModuleDto[]; }
    catch { return []; }
  }
}
