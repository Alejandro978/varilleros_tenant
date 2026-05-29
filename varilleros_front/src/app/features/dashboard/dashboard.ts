import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

interface DashboardCard {
  title: string;
  description: string;
  icon: string;
  route: string;
  color: string;
}

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, CardModule, ButtonModule],
  templateUrl: './dashboard.html',
})
export class DashboardComponent {
  cards: DashboardCard[] = [
    { title: 'Clientes', description: 'Gestiona los clientes del taller', icon: 'pi pi-users', route: '/clientes', color: 'text-blue-500' },
    { title: 'Peritos', description: 'Peritos de seguros asignados', icon: 'pi pi-id-card', route: '/peritos', color: 'text-green-500' },
    { title: 'Artículos', description: 'Catálogo de artículos y repuestos', icon: 'pi pi-box', route: '/articulos', color: 'text-orange-500' },
    { title: 'Precios', description: 'Tarifa de precios por abolladuras', icon: 'pi pi-euro', route: '/precios', color: 'text-yellow-500' },
    { title: 'Presupuestos', description: 'Presupuestos de reparación PDR', icon: 'pi pi-file-pdf', route: '/presupuestos', color: 'text-purple-500' },
    { title: 'Admin', description: 'Gestión de tenants y módulos', icon: 'pi pi-cog', route: '/admin/tenants', color: 'text-slate-500' },
  ];
}
