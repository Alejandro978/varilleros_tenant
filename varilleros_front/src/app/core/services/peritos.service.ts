import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { PeritoDto, CreatePeritoDto, UpdatePeritoDto } from '../models/perito.model';

@Injectable({ providedIn: 'root' })
export class PeritosService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/peritos`;

  getAll() { return this.http.get<PeritoDto[]>(this.base); }
  getById(id: number) { return this.http.get<PeritoDto>(`${this.base}/${id}`); }
  create(dto: CreatePeritoDto) { return this.http.post<number>(this.base, dto); }
  update(id: number, dto: UpdatePeritoDto) { return this.http.put<void>(`${this.base}/${id}`, dto); }
  delete(id: number) { return this.http.delete<void>(`${this.base}/${id}`); }
}
