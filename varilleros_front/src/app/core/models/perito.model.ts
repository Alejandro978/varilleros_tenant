export interface PeritoDto {
  id: number;
  nombre: string;
  email: string;
}

export interface CreatePeritoDto {
  nombre: string;
  email: string;
}

export interface UpdatePeritoDto extends CreatePeritoDto {
  id: number;
}
