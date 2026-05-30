import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { moduleGuard } from './core/guards/module.guard';

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
      // ── Rutas protegidas por módulo ──────────────────────────────────────
      {
        path: 'clientes',
        canActivate: [moduleGuard],
        data: { module: 'CLIENTES' },
        loadComponent: () =>
          import('./features/clientes/clientes-list/clientes-list').then(m => m.ClientesListComponent),
      },
      {
        path: 'peritos',
        canActivate: [moduleGuard],
        data: { module: 'PERITOS' },
        loadComponent: () =>
          import('./features/peritos/peritos-list/peritos-list').then(m => m.PeritosListComponent),
      },
      {
        path: 'articulos',
        canActivate: [moduleGuard],
        data: { module: 'ARTICULOS' },
        loadComponent: () =>
          import('./features/articulos/articulos-list/articulos-list').then(m => m.ArticulosListComponent),
      },
      {
        path: 'precios',
        canActivate: [moduleGuard],
        data: { module: 'PRECIOS' },
        loadComponent: () =>
          import('./features/precios/precios-list/precios-list').then(m => m.PreciosListComponent),
      },
      {
        path: 'presupuestos',
        canActivate: [moduleGuard],
        data: { module: 'PRESUPUESTOS' },
        loadComponent: () =>
          import('./features/presupuestos/presupuestos-list/presupuestos-list').then(m => m.PresupuestosListComponent),
      },
      // ── Rutas de administración — requieren módulo ADMIN ────────────────
      {
        path: 'admin/tenants',
        canActivate: [moduleGuard],
        data: { module: 'ADMIN' },
        loadComponent: () =>
          import('./features/admin/tenants/tenants-list/tenants-list').then(m => m.TenantsListComponent),
      },
      {
        path: 'admin/modules',
        canActivate: [moduleGuard],
        data: { module: 'ADMIN' },
        loadComponent: () =>
          import('./features/admin/modules/modules-list/modules-list').then(m => m.ModulesListComponent),
      },
      {
        path: 'admin/tenants/:id/modules',
        canActivate: [moduleGuard],
        data: { module: 'ADMIN' },
        loadComponent: () =>
          import('./features/admin/tenant-modules/tenant-modules/tenant-modules').then(m => m.TenantModulesComponent),
      },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];
