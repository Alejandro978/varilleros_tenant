import { Component, computed, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../../core/services/auth.service';

// Módulos de negocio que aparecen como ítems directos en el menú
const MODULE_MENU: Record<string, { label: string; icon: string; routerLink: string }> = {
  clientes:     { label: 'Clientes',     icon: 'pi pi-users',    routerLink: '/clientes' },
  peritos:      { label: 'Peritos',      icon: 'pi pi-id-card',  routerLink: '/peritos' },
  articulos:    { label: 'Artículos',    icon: 'pi pi-box',      routerLink: '/articulos' },
  precios:      { label: 'Precios',      icon: 'pi pi-euro',     routerLink: '/precios' },
  presupuestos: { label: 'Presupuestos', icon: 'pi pi-file-pdf', routerLink: '/presupuestos' },
};

@Component({
  selector: 'app-sidebar',
  imports: [RouterModule, PanelMenuModule],
  templateUrl: './sidebar.html',
})
export class SidebarComponent {
  private auth = inject(AuthService);

  menuItems = computed<MenuItem[]>(() => {
    const codes = this.auth.modulesSignal().map(m => m.code.toLowerCase());

    // Ítems de negocio — solo los módulos activos del tenant
    const moduleItems: MenuItem[] = codes
      .map(code => MODULE_MENU[code])
      .filter(Boolean);

    // Sección Admin — solo visible si el tenant tiene el módulo ADMIN
    const hasAdmin = codes.includes('admin');
    const adminSection: MenuItem[] = hasAdmin
      ? [
          { separator: true },
          {
            label: 'Admin',
            icon: 'pi pi-cog',
            items: [
              { label: 'Tenants',            icon: 'pi pi-building', routerLink: '/admin/tenants' },
              { label: 'Módulos',            icon: 'pi pi-puzzle',   routerLink: '/admin/modules' },
              { label: 'Módulos por Tenant', icon: 'pi pi-link',     routerLink: '/admin/tenant-modules' },
            ],
          },
        ]
      : [];

    return [
      { label: 'Dashboard', icon: 'pi pi-chart-bar', routerLink: '/dashboard' },
      ...moduleItems,
      ...adminSection,
    ];
  });
}
