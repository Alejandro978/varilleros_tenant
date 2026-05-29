export interface ActiveModuleDto {
  code: string;
  name: string;
}

export interface ModuleCatalogDto {
  id: number;
  code: string;
  name: string;
  description: string;
  isActive: boolean;
}

export interface CreateModuleCatalogDto {
  code: string;
  name: string;
  description: string;
}

export interface TenantModuleDto {
  id: number;
  tenantId: number;
  moduleId: number;
  moduleName: string;
  moduleCode: string;
  isActive: boolean;
  grantedAt?: string | null;
  expiresAt?: string | null;
}

export interface CreateTenantModuleDto {
  tenantId: number;
  moduleId: number;
  expiresAt?: string | null;
}
