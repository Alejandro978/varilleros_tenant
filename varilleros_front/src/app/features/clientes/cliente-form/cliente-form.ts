import { Component, inject, input, output, effect } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { MessageService } from 'primeng/api';
import { ClientesService } from '../../../core/services/clientes.service';
import { ClienteDto } from '../../../core/models/cliente.model';

@Component({
  selector: 'app-cliente-form',
  imports: [ReactiveFormsModule, ButtonModule, InputText],
  templateUrl: './cliente-form.html',
})
export class ClienteFormComponent {
  item = input<ClienteDto | null>(null);
  saved = output<void>();
  cancelled = output<void>();

  private fb = inject(FormBuilder);
  private service = inject(ClientesService);
  private messageService = inject(MessageService);

  form = this.fb.group({
    nombreCliente: ['', Validators.required],
    nifCif: ['', Validators.required],
    direccion: ['', Validators.required],
    poblacion: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    telefono: ['', Validators.required],
  });

  constructor() {
    effect(() => {
      const item = this.item();
      item ? this.form.patchValue(item) : this.form.reset();
    });
  }

  save(): void {
    if (this.form.invalid) return;
    const v = this.form.value as any;
    const item = this.item();
    const call: Observable<unknown> = item
      ? this.service.update(item.id, { id: item.id, ...v })
      : this.service.create(v);

    call.subscribe({
      next: () => { this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Cliente guardado' }); this.saved.emit(); },
      error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo guardar' }); },
    });
  }
}
