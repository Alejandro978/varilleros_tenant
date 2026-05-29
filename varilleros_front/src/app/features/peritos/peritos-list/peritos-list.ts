import { Component, inject, OnInit, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PeritosService } from '../../../core/services/peritos.service';
import { PeritoDto } from '../../../core/models/perito.model';
import { PeritoFormComponent } from '../perito-form/perito-form';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-peritos-list',
  imports: [TableModule, ButtonModule, DialogModule, PeritoFormComponent, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './peritos-list.html',
})
export class PeritosListComponent implements OnInit {
  private service = inject(PeritosService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);

  items = signal<PeritoDto[]>([]);
  loading = signal(false);
  showForm = signal(false);
  editing = signal<PeritoDto | null>(null);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  openCreate(): void { this.editing.set(null); this.showForm.set(true); }
  openEdit(item: PeritoDto): void { this.editing.set(item); this.showForm.set(true); }
  onSaved(): void { this.showForm.set(false); this.load(); }

  confirmDelete(id: number): void {
    this.confirmationService.confirm({
      message: '¿Seguro que deseas eliminar este perito?',
      header: 'Confirmar eliminación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.delete(id).subscribe({
          next: () => { this.messageService.add({ severity: 'success', summary: 'Eliminado', detail: 'Perito eliminado' }); this.load(); },
          error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo eliminar' }); },
        });
      },
    });
  }
}
