import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { TenantDto, CreateTenantDto, UpdateTenantDto } from '../models/tenant.model';

@Injectable({ providedIn: 'root' })
export class TenantsService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/admin/tenants`;

  getAll() { return this.http.get<TenantDto[]>(this.base); }
  getById(id: number) { return this.http.get<TenantDto>(`${this.base}/${id}`); }
  create(dto: CreateTenantDto) { return this.http.post<number>(this.base, dto); }
  update(id: number, dto: UpdateTenantDto) { return this.http.put<void>(`${this.base}/${id}`, dto); }
  setPassword(id: number, newPassword: string) { return this.http.put<void>(`${this.base}/${id}/password`, { newPassword }); }
  delete(id: number) { return this.http.delete<void>(`${this.base}/${id}`); }
}
