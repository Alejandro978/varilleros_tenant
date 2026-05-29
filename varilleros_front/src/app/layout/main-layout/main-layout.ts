import { Component, inject, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { DrawerModule } from 'primeng/drawer';
import { TopbarComponent } from '../topbar/topbar';
import { SidebarComponent } from '../sidebar/sidebar';

@Component({
  selector: 'app-main-layout',
  imports: [RouterModule, DrawerModule, TopbarComponent, SidebarComponent],
  templateUrl: './main-layout.html',
})
export class MainLayoutComponent {
  drawerVisible = signal(false);

  constructor() {
    const router = inject(Router);
    router.events.pipe(filter(e => e instanceof NavigationEnd)).subscribe(() => {
      this.drawerVisible.set(false);
    });
  }
}
