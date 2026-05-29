import { Component, inject, input, output, effect } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { MessageService } from 'primeng/api';
import { ModulesService } from '../../../../core/services/modules.service';
import { ModuleCatalogDto } from '../../../../core/models/module.model';

@Component({
  selector: 'app-module-form',
  imports: [ReactiveFormsModule, ButtonModule, InputText, ToggleButtonModule],
  templateUrl: './module-form.html',
})
export class ModuleFormComponent {
  item = input<ModuleCatalogDto | null>(null);
  saved = output<void>();
  cancelled = output<void>();

  private fb = inject(FormBuilder);
  private service = inject(ModulesService);
  private messageService = inject(MessageService);

  form = this.fb.group({
    code: ['', Validators.required],
    name: ['', Validators.required],
    description: [''],
    isActive: [true],
  });

  constructor() {
    effect(() => {
      const item = this.item();
      if (item) {
        this.form.patchValue(item);
        this.form.get('code')?.disable();
      } else {
        this.form.reset({ isActive: true });
        this.form.get('code')?.enable();
      }
    });
  }

  save(): void {
    if (this.form.invalid) return;
    const v = this.form.getRawValue() as any;
    this.service.create({ code: v.code, name: v.name, description: v.description }).subscribe({
      next: () => { this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Módulo guardado' }); this.saved.emit(); },
      error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo guardar' }); },
    });
  }
}
