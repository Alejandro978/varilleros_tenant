export interface TenantDto {
  id: number;
  name: string;
  slug: string;
  isActive: boolean;
}

export interface CreateTenantDto {
  name: string;
  slug: string;
  dbHost: string;
  dbPort: number;
  dbName: string;
  dbUser: string;
  dbPassword: string;
  appPassword: string;
}

export interface UpdateTenantDto {
  id: number;
  name: string;
  dbHost: string;
  dbPort: number;
  dbName: string;
  dbUser: string;
  dbPassword: string;
}
