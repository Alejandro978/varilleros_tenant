import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login').then(m => m.LoginComponent),
  },
  {
    path: '',
    loadComponent: () =>
      import('./layout/main-layout/main-layout').then(m => m.MainLayoutComponent),
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard').then(m => m.DashboardComponent),
      },
      {
        path: 'clientes',
        loadComponent: () =>
          import('./features/clientes/clientes-list/clientes-list').then(m => m.ClientesListComponent),
      },
      {
        path: 'peritos',
        loadComponent: () =>
          import('./features/peritos/peritos-list/peritos-list').then(m => m.PeritosListComponent),
      },
      {
        path: 'articulos',
        loadComponent: () =>
          import('./features/articulos/articulos-list/articulos-list').then(m => m.ArticulosListComponent),
      },
      {
        path: 'precios',
        loadComponent: () =>
          import('./features/precios/precios-list/precios-list').then(m => m.PreciosListComponent),
      },
      {
        path: 'presupuestos',
        loadComponent: () =>
          import('./features/presupuestos/presupuestos-list/presupuestos-list').then(m => m.PresupuestosListComponent),
      },
      {
        path: 'admin/tenants',
        loadComponent: () =>
          import('./features/admin/tenants/tenants-list/tenants-list').then(m => m.TenantsListComponent),
      },
      {
        path: 'admin/modules',
        loadComponent: () =>
          import('./features/admin/modules/modules-list/modules-list').then(m => m.ModulesListComponent),
      },
      {
        path: 'admin/tenants/:id/modules',
        loadComponent: () =>
          import('./features/admin/tenant-modules/tenant-modules/tenant-modules').then(m => m.TenantModulesComponent),
      },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];
