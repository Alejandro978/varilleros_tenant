import { Component, inject, output } from '@angular/core';
import { UpperCasePipe } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { AvatarModule } from 'primeng/avatar';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-topbar',
  imports: [UpperCasePipe, ButtonModule, AvatarModule],
  templateUrl: './topbar.html',
})
export class TopbarComponent {
  menuToggle = output<void>();

  protected auth = inject(AuthService);
}
