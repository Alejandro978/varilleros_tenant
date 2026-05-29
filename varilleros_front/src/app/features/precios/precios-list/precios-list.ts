import { Component, inject, OnInit, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { PreciosService } from '../../../core/services/precios.service';
import { PrecioDto } from '../../../core/models/precio.model';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-precios-list',
  imports: [TableModule, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './precios-list.html',
})
export class PreciosListComponent implements OnInit {
  private service = inject(PreciosService);

  items = signal<PrecioDto[]>([]);
  loading = signal(false);

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading.set(true);
    this.service.getAll().subscribe({
      next: data => { this.items.set(data); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
