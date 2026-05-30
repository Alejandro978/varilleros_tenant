import { Component, inject, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { DrawerModule } from 'primeng/drawer';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { TopbarComponent } from '../topbar/topbar';
import { SidebarComponent } from '../sidebar/sidebar';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-main-layout',
  imports: [RouterModule, DrawerModule, ConfirmDialogModule, ToastModule, TopbarComponent, SidebarComponent],
  templateUrl: './main-layout.html',
})
export class MainLayoutComponent {
  drawerVisible = signal(false);

  constructor() {
    const router = inject(Router);
    const auth   = inject(AuthService);

    router.events.pipe(filter(e => e instanceof NavigationEnd)).subscribe(() => {
      this.drawerVisible.set(false);
      // Refresca módulos en cada navegación → el sidebar se actualiza sin re-login
      auth.refreshModules().subscribe();
    });
  }
}
