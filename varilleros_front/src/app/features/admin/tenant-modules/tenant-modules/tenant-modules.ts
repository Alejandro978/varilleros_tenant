import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { SelectModule } from 'primeng/select';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ModulesService } from '../../../../core/services/modules.service';
import { TenantModuleDto, ModuleCatalogDto } from '../../../../core/models/module.model';
import { PageHeaderComponent } from '../../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-tenant-modules',
  imports: [RouterLink, TableModule, ButtonModule, TagModule, SelectModule, ReactiveFormsModule, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './tenant-modules.html',
})
export class TenantModulesComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private service = inject(ModulesService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);
  private fb = inject(FormBuilder);

  tenantId = signal(0);
  assignedModules = signal<TenantModuleDto[]>([]);
  allModules = signal<ModuleCatalogDto[]>([]);
  loading = signal(false);

  assignForm = this.fb.group({ moduleId: [null as number | null] });

  get availableModules() {
    const assigned = new Set(this.assignedModules().map(m => m.moduleId));
    return this.allModules().filter(m => !assigned.has(m.id));
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.tenantId.set(id);
    this.loadAll();
  }

  loadAll(): void {
    this.loading.set(true);
    const id = this.tenantId();
    this.service.getAll().subscribe(all => this.allModules.set(all));
    this.service.getModulesByTenant(id).subscribe({
      next: data => { this.assignedModules.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  assign(): void {
    const moduleId = this.assignForm.value.moduleId;
    if (!moduleId) return;
    this.service.assignModule(this.tenantId(), moduleId, { tenantId: this.tenantId(), moduleId }).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Asignado', detail: 'Módulo asignado' });
        this.assignForm.reset();
        this.loadAll();
      },
      error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo asignar' }); },
    });
  }

  revoke(moduleId: number): void {
    this.confirmationService.confirm({
      message: '¿Revocar acceso a este módulo?',
      header: 'Confirmar',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.service.revokeModule(this.tenantId(), moduleId).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Revocado', detail: 'Módulo revocado' });
            this.loadAll();
          },
          error: () => { this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo revocar' }); },
        });
      },
    });
  }
}
