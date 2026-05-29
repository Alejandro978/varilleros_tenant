import { Component, inject, input, output, effect } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { MessageService } from 'primeng/api';
import { ArticulosService } from '../../../core/services/articulos.service';
import { ArticuloDto } from '../../../core/models/articulo.model';

@Component({
  selector: 'app-articulo-form',
  imports: [ReactiveFormsModule, ButtonModule, InputText],
  templateUrl: './articulo-form.html',
})
export class ArticuloFormComponent {
  item = input<ArticuloDto | null>(null);
  saved = output<void>();
  cancelled = output<void>();

  private fb = inject(FormBuilder);
  private service = inject(ArticulosService);
  private messageService = inject(MessageService);

  form = this.fb.group({
    codigo: ['', Validators.required],
    descripcion: ['', Validators.required],
    codigoPrecioPresupuesto: [''],
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
      next: () => { this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Artículo guardado' }); this.saved.emit(); },
      error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo guardar' }); },
    });
  }
}
