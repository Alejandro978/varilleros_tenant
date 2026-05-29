import { Component, inject, OnInit, signal } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { PresupuestosService } from '../../../core/services/presupuestos.service';
import { PresupuestoDto, ESTADO_LABELS, ESTADO_SEVERITIES } from '../../../core/models/presupuesto.model';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-presupuestos-list',
  imports: [DecimalPipe, TableModule, TagModule, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './presupuestos-list.html',
})
export class PresupuestosListComponent implements OnInit {
  private service = inject(PresupuestosService);

  items = signal<PresupuestoDto[]>([]);
  loading = signal(false);

  estadoLabel = (estado: number) => ESTADO_LABELS[estado] ?? 'Desconocido';
  estadoSeverity = (estado: number): any => ESTADO_SEVERITIES[estado] ?? 'secondary';

  fechaToDate = (ts: number) => ts ? new Date(ts * 1000).toLocaleDateString('es-ES') : '—';

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
