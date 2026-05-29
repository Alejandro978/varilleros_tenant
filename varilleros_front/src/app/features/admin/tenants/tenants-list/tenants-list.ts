import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TenantsService } from '../../../../core/services/tenants.service';
import { TenantDto } from '../../../../core/models/tenant.model';
import { TenantFormComponent } from '../tenant-form/tenant-form';
import { PageHeaderComponent } from '../../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-tenants-list',
  imports: [RouterLink, TableModule, ButtonModule, DialogModule, TagModule, TenantFormComponent, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './tenants-list.html',
})
export class TenantsListComponent implements OnInit {
  private service = inject(TenantsService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);

  items = signal<TenantDto[]>([]);
  loading = signal(false);
  showForm = signal(false);
  editing = signal<TenantDto | null>(null);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  openCreate(): void { this.editing.set(null); this.showForm.set(true); }
  openEdit(item: TenantDto): void { this.editing.set(item); this.showForm.set(true); }
  onSaved(): void { this.showForm.set(false); this.load(); }

  confirmDelete(id: number): void {
    this.confirmationService.confirm({
      message: '¿Seguro que deseas eliminar este tenant? Esta acción no se puede deshacer.',
      header: 'Confirmar eliminación',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.delete(id).subscribe({
          next: () => { this.messageService.add({ severity: 'success', summary: 'Eliminado', detail: 'Tenant eliminado' }); this.load(); },
          error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo eliminar' }); },
        });
      },
    });
  }
}
