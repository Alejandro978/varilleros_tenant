import { Component, inject, OnInit, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { MessageService } from 'primeng/api';
import { ModulesService } from '../../../../core/services/modules.service';
import { ModuleCatalogDto } from '../../../../core/models/module.model';
import { ModuleFormComponent } from '../module-form/module-form';
import { PageHeaderComponent } from '../../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-modules-list',
  imports: [TableModule, ButtonModule, DialogModule, TagModule, ModuleFormComponent, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './modules-list.html',
})
export class ModulesListComponent implements OnInit {
  private service = inject(ModulesService);
  private messageService = inject(MessageService);

  items = signal<ModuleCatalogDto[]>([]);
  loading = signal(false);
  showForm = signal(false);
  editing = signal<ModuleCatalogDto | null>(null);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  openCreate(): void { this.editing.set(null); this.showForm.set(true); }
  openEdit(item: ModuleCatalogDto): void { this.editing.set(item); this.showForm.set(true); }
  onSaved(): void { this.showForm.set(false); this.load(); }
}
