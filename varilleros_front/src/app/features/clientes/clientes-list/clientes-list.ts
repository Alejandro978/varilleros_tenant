import { Component, inject, OnInit, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ClientesService } from '../../../core/services/clientes.service';
import { ClienteDto } from '../../../core/models/cliente.model';
import { ClienteFormComponent } from '../cliente-form/cliente-form';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-clientes-list',
  imports: [TableModule, ButtonModule, DialogModule, TagModule, ClienteFormComponent, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './clientes-list.html',
})
export class ClientesListComponent implements OnInit {
  private service = inject(ClientesService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);

  items = signal<ClienteDto[]>([]);
  loading = signal(false);
  showForm = signal(false);
  editing = signal<ClienteDto | null>(null);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  openCreate(): void { this.editing.set(null); this.showForm.set(true); }
  openEdit(item: ClienteDto): void { this.editing.set(item); this.showForm.set(true); }
  onSaved(): void { this.showForm.set(false); this.load(); }

  confirmDelete(id: number): void {
    this.confirmationService.confirm({
      message: '¿Seguro que deseas eliminar este cliente?',
      header: 'Confirmar eliminación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.delete(id).subscribe({
          next: () => { this.messageService.add({ severity: 'success', summary: 'Eliminado', detail: 'Cliente eliminado' }); this.load(); },
          error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo eliminar' }); },
        });
      },
    });
  }
}
