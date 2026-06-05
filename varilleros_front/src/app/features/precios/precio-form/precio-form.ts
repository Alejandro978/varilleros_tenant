import { Component, inject, input, output, effect } from '@angular/core';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { PreciosService } from '../../../core/services/precios.service';
import { PrecioDto } from '../../../core/models/precio.model';

@Component({
  selector: 'app-precio-form',
  imports: [ReactiveFormsModule, InputNumberModule, ButtonModule],
  templateUrl: './precio-form.html',
})
export class PrecioFormComponent {
  item = input<PrecioDto | null>(null);
  saved = output<void>();
  cancelled = output<void>();

  private fb             = inject(FormBuilder);
  private service        = inject(PreciosService);
  private messageService = inject(MessageService);

  form = this.fb.group({
    aletaLeve:    [null as number | null],
    aletaMedio:   [null as number | null],
    aletaGrave:   [null as number | null],
    puertaLeve:   [null as number | null],
    puertaMedio:  [null as number | null],
    puertaGrave:  [null as number | null],
    techoLeve:    [null as number | null],
    techoMedio:   [null as number | null],
    techoGrave:   [null as number | null],
    capoLeve:     [null as number | null],
    capoMedio:    [null as number | null],
    capoGrave:    [null as number | null],
    portonLeve:   [null as number | null],
    portonMedio:  [null as number | null],
    portonGrave:  [null as number | null],
    montanteLeve:  [null as number | null],
    montanteMedio: [null as number | null],
    montanteGrave: [null as number | null],
  });

  constructor() {
    effect(() => {
      const item = this.item();
      item ? this.form.patchValue(item) : this.form.reset();
    });
  }

  save(): void {
    const item = this.item();
    if (!item) return;

    const v = this.form.value;
    const dto: PrecioDto = {
      numeroabolladuras: item.numeroabolladuras,
      aletaLeve:    v.aletaLeve    ?? null,
      aletaMedio:   v.aletaMedio   ?? null,
      aletaGrave:   v.aletaGrave   ?? null,
      puertaLeve:   v.puertaLeve   ?? null,
      puertaMedio:  v.puertaMedio  ?? null,
      puertaGrave:  v.puertaGrave  ?? null,
      techoLeve:    v.techoLeve    ?? null,
      techoMedio:   v.techoMedio   ?? null,
      techoGrave:   v.techoGrave   ?? null,
      capoLeve:     v.capoLeve     ?? null,
      capoMedio:    v.capoMedio    ?? null,
      capoGrave:    v.capoGrave    ?? null,
      portonLeve:   v.portonLeve   ?? null,
      portonMedio:  v.portonMedio  ?? null,
      portonGrave:  v.portonGrave  ?? null,
      montanteLeve:  v.montanteLeve  ?? null,
      montanteMedio: v.montanteMedio ?? null,
      montanteGrave: v.montanteGrave ?? null,
    };

    this.service.update(item.numeroabolladuras, dto).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Guardado', detail: 'Precios actualizados' });
        this.saved.emit();
      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudieron guardar los precios' });
      },
    });
  }
}
