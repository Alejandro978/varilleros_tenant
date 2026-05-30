import { Component, computed, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../../core/services/auth.service';

const MODULE_MENU: Record<string, { label: string; icon: string; routerLink: string }> = {
  clientes:     { label: 'Clientes',    icon: 'pi pi-users',    routerLink: '/clientes' },
  peritos:      { label: 'Peritos',     icon: 'pi pi-id-card',  routerLink: '/peritos' },
  articulos:    { label: 'Artículos',   icon: 'pi pi-box',      routerLink: '/articulos' },
  precios:      { label: 'Precios',     icon: 'pi pi-euro',     routerLink: '/precios' },
  presupuestos: { label: 'Presupuestos',icon: 'pi pi-file-pdf', routerLink: '/presupuestos' },
};

@Component({
  selector: 'app-sidebar',
  imports: [RouterModule, PanelMenuModule],
  templateUrl: './sidebar.html',
})
export class SidebarComponent {
  private auth = inject(AuthService);

  // computed() se recalcula automáticamente si cambia el signal de módulos
  menuItems = computed<MenuItem[]>(() => {
    const moduleItems: MenuItem[] = this.auth.modulesSignal()
      .map(m => MODULE_MENU[m.code.toLowerCase()])
      .filter(Boolean);

    return [
      { label: 'Dashboard', icon: 'pi pi-chart-bar', routerLink: '/dashboard' },
      ...moduleItems,
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
    ];
  });
}
