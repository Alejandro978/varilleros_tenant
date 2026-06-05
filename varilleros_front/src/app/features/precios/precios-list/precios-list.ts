import { Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Table, TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TooltipModule } from 'primeng/tooltip';
import { PreciosService } from '../../../core/services/precios.service';
import { PrecioDto } from '../../../core/models/precio.model';
import { PrecioFormComponent } from '../precio-form/precio-form';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-precios-list',
  imports: [
    FormsModule, TableModule, ButtonModule, DialogModule,
    InputTextModule, TooltipModule,
    PrecioFormComponent, PageHeaderComponent, EmptyStateComponent,
  ],
  templateUrl: './precios-list.html',
})
export class PreciosListComponent implements OnInit {
  private service = inject(PreciosService);

  @ViewChild('dt') dt!: Table;

  items    = signal<PrecioDto[]>([]);
  loading  = signal(false);
  showForm = signal(false);
  editing  = signal<PrecioDto | null>(null);
  filterValue = '';

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  applyFilter(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.dt.filterGlobal(value, 'equals');
  }

  clearFilter(): void {
    this.filterValue = '';
    this.dt.filterGlobal('', 'equals');
  }

  openEdit(item: PrecioDto): void {
    this.editing.set(item);
    this.showForm.set(true);
  }

  onSaved(): void {
    this.showForm.set(false);
    this.load();
  }
}
