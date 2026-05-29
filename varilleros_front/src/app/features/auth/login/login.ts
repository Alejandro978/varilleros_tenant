import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { Password } from 'primeng/password';
import { CardModule } from 'primeng/card';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, ButtonModule, InputText, Password, CardModule],
  templateUrl: './login.html',
})
export class LoginComponent {
  private auth           = inject(AuthService);
  private router         = inject(Router);
  private messageService = inject(MessageService);
  private fb             = inject(FormBuilder);

  loading = false;

  form = this.fb.group({
    slug:     ['', Validators.required],
    password: ['', Validators.required],
  });

  submit(): void {
    if (this.form.invalid) return;
    this.loading = true;

    const { slug, password } = this.form.value;
    this.auth.login(slug!, password!).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: (err) => {
        this.loading = false;
        const msg = err.status === 401
          ? 'Empresa o contraseña incorrectos'
          : 'Error de conexión. Comprueba que la API está activa.';
        this.messageService.add({ severity: 'error', summary: 'Error de acceso', detail: msg, life: 5000 });
      },
    });
  }
}
