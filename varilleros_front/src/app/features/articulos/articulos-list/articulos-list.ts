import { Component, inject, OnInit, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ArticulosService } from '../../../core/services/articulos.service';
import { ArticuloDto } from '../../../core/models/articulo.model';
import { PageHeaderComponent } from '../../../shared/components/page-header/page-header';
import { EmptyStateComponent } from '../../../shared/components/empty-state/empty-state';

@Component({
  selector: 'app-articulos-list',
  imports: [TableModule, PageHeaderComponent, EmptyStateComponent],
  templateUrl: './articulos-list.html',
})
export class ArticulosListComponent implements OnInit {
  private service = inject(ArticulosService);

  items   = signal<ArticuloDto[]>([]);
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
