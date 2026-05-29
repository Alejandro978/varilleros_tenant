import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-sidebar',
  imports: [RouterModule, PanelMenuModule],
  templateUrl: './sidebar.html',
})
export class SidebarComponent {
  menuItems: MenuItem[] = [
    { label: 'Dashboard', icon: 'pi pi-chart-bar', routerLink: '/dashboard' },
    { label: 'Clientes', icon: 'pi pi-users', routerLink: '/clientes' },
    { label: 'Peritos', icon: 'pi pi-id-card', routerLink: '/peritos' },
    { label: 'Artículos', icon: 'pi pi-box', routerLink: '/articulos' },
    { label: 'Precios', icon: 'pi pi-euro', routerLink: '/precios' },
    { label: 'Presupuestos', icon: 'pi pi-file-pdf', routerLink: '/presupuestos' },
    { separator: true },
    {
      label: 'Admin',
      icon: 'pi pi-cog',
      items: [
        { label: 'Tenants', icon: 'pi pi-building', routerLink: '/admin/tenants' },
        { label: 'Módulos', icon: 'pi pi-puzzle', routerLink: '/admin/modules' },
        { label: 'Módulos por Tenant', icon: 'pi pi-link', routerLink: '/admin/tenants' },
      ],
    },
  ];
}
