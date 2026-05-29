import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ModuleCatalogDto, CreateModuleCatalogDto, TenantModuleDto, CreateTenantModuleDto } from '../models/module.model';

@Injectable({ providedIn: 'root' })
export class ModulesService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/admin/modules`;
  private readonly tenantsBase = `${environment.apiUrl}/api/admin/tenants`;

  getAll() { return this.http.get<ModuleCatalogDto[]>(this.base); }
  create(dto: CreateModuleCatalogDto) { return this.http.post<number>(this.base, dto); }

  getModulesByTenant(tenantId: number) {
    return this.http.get<TenantModuleDto[]>(`${this.tenantsBase}/${tenantId}/modules`);
  }

  assignModule(tenantId: number, moduleId: number, dto: CreateTenantModuleDto) {
    return this.http.post<void>(`${this.tenantsBase}/${tenantId}/modules/${moduleId}`, dto);
  }

  revokeModule(tenantId: number, moduleId: number) {
    return this.http.delete<void>(`${this.tenantsBase}/${tenantId}/modules/${moduleId}`);
  }
}
