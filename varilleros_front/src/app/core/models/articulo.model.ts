export interface ArticuloDto {
  id: number;
  codigo: string;
  descripcion: string;
  codigoPrecioPresupuesto: string;
}

export interface CreateArticuloDto {
  codigo: string;
  descripcion: string;
  codigoPrecioPresupuesto: string;
}

export interface UpdateArticuloDto extends CreateArticuloDto {
  id: number;
}
