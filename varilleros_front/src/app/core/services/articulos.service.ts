import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ArticuloDto, CreateArticuloDto, UpdateArticuloDto } from '../models/articulo.model';

@Injectable({ providedIn: 'root' })
export class ArticulosService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/articulos`;

  getAll() { return this.http.get<ArticuloDto[]>(this.base); }
  getById(id: number) { return this.http.get<ArticuloDto>(`${this.base}/${id}`); }
  create(dto: CreateArticuloDto) { return this.http.post<number>(this.base, dto); }
  update(id: number, dto: UpdateArticuloDto) { return this.http.put<void>(`${this.base}/${id}`, dto); }
  delete(id: number) { return this.http.delete<void>(`${this.base}/${id}`); }
}
