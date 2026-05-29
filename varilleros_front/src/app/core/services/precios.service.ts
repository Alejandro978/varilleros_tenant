import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { PrecioDto } from '../models/precio.model';

@Injectable({ providedIn: 'root' })
export class PreciosService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/precios`;

  getAll() { return this.http.get<PrecioDto[]>(this.base); }
  getById(numeroabolladuras: number) { return this.http.get<PrecioDto>(`${this.base}/${numeroabolladuras}`); }
  create(dto: PrecioDto) { return this.http.post<void>(this.base, dto); }
  update(numeroabolladuras: number, dto: PrecioDto) { return this.http.put<void>(`${this.base}/${numeroabolladuras}`, dto); }
  delete(numeroabolladuras: number) { return this.http.delete<void>(`${this.base}/${numeroabolladuras}`); }
}
