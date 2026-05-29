import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

interface LoginResponse {
  accessToken: string;
  tenantName: string;
  slug: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http   = inject(HttpClient);
  private readonly router = inject(Router);

  login(slug: string, password: string): Observable<void> {
    return this.http
      .post<LoginResponse>(`${environment.apiUrl}/api/auth/login`, { slug, password })
      .pipe(
        tap(res => {
          localStorage.setItem('access_token', res.accessToken);
          localStorage.setItem('tenant_slug',  res.slug);
          localStorage.setItem('tenant_name',  res.tenantName);
          localStorage.setItem('user_email',   res.slug);
        }),
        map(() => void 0)
      );
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

  getTenantName(): string {
    return localStorage.getItem('tenant_name') ?? '';
  }

  getUserEmail(): string {
    return localStorage.getItem('user_email') ?? '';
  }
}
