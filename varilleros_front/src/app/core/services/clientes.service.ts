import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ClienteDto, CreateClienteDto, UpdateClienteDto } from '../models/cliente.model';

@Injectable({ providedIn: 'root' })
export class ClientesService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api/clientes`;

  getAll() { return this.http.get<ClienteDto[]>(this.base); }
  getById(id: number) { return this.http.get<ClienteDto>(`${this.base}/${id}`); }
  create(dto: CreateClienteDto) { return this.http.post<number>(this.base, dto); }
  update(id: number, dto: UpdateClienteDto) { return this.http.put<void>(`${this.base}/${id}`, dto); }
  delete(id: number) { return this.http.delete<void>(`${this.base}/${id}`); }
}
