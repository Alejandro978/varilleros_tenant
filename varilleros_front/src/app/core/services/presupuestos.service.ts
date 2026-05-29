import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { PresupuestoDto } from '../models/presupuesto.model';

@Injectable({ providedIn: 'root' })
export class PresupuestosService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/presupuestos`;

  getAll() { return this.http.get<PresupuestoDto[]>(this.base); }
  getById(id: number) { return this.http.get<PresupuestoDto>(`${this.base}/${id}`); }
  delete(id: number) { return this.http.delete<void>(`${this.base}/${id}`); }
}
