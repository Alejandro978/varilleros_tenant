export interface ClienteDto {
  id: number;
  nombreCliente: string;
  nifCif: string;
  direccion: string;
  poblacion: string;
  email: string;
  telefono: string;
}

export interface CreateClienteDto {
  nombreCliente: string;
  nifCif: string;
  direccion: string;
  poblacion: string;
  email: string;
  telefono: string;
}

export interface UpdateClienteDto extends CreateClienteDto {
  id: number;
}
