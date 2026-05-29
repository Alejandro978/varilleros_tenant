import { Component, inject, input, output, effect } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Observable, forkJoin, of } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { Password } from 'primeng/password';
import { MessageService } from 'primeng/api';
import { TenantsService } from '../../../../core/services/tenants.service';
import { TenantDto } from '../../../../core/models/tenant.model';

@Component({
  selector: 'app-tenant-form',
  imports: [ReactiveFormsModule, ButtonModule, InputText, InputNumberModule, Password],
  templateUrl: './tenant-form.html',
})
export class TenantFormComponent {
  item = input<TenantDto | null>(null);
  saved = output<void>();
  cancelled = output<void>();

  private fb = inject(FormBuilder);
  private service = inject(TenantsService);
  private messageService = inject(MessageService);

  form = this.fb.group({
    name:        ['', Validators.required],
    slug:        ['', Validators.required],
    dbHost:      ['localhost', Validators.required],
    dbPort:      [3306, Validators.required],
    dbName:      ['', Validators.required],
    dbUser:      ['root', Validators.required],
    dbPassword:  [''],
    appPassword: ['', Validators.required],  // contraseña de acceso a la app
  });

  constructor() {
    effect(() => {
      const item = this.item();
      if (item) {
        this.form.patchValue(item);
        this.form.get('slug')?.disable();
        this.form.get('appPassword')?.clearValidators();  // no obligatoria al editar
        this.form.get('appPassword')?.updateValueAndValidity();
      } else {
        this.form.reset({ dbHost: 'localhost', dbPort: 3306, dbUser: 'root' });
        this.form.get('slug')?.enable();
        this.form.get('appPassword')?.setValidators(Validators.required);
        this.form.get('appPassword')?.updateValueAndValidity();
      }
    });
  }

  save(): void {
    if (this.form.invalid) return;
    const v = this.form.getRawValue() as any;
    const item = this.item();

    let call: Observable<unknown>;
    if (item) {
      const update$ = this.service.update(item.id, { id: item.id, ...v });
      const pw$ = v.appPassword ? this.service.setPassword(item.id, v.appPassword) : of(null);
      call = forkJoin([update$, pw$]);
    } else {
      call = this.service.create(v);
    }

    call.subscribe({
      next: () => { this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Tenant guardado' }); this.saved.emit(); },
      error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo guardar' }); },
    });
  }
}
