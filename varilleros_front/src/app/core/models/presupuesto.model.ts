export interface PresupuestoDto {
  id: number;
  nombreCliente: string;
  idPerito: number;
  precioTotal: number;
  fechaCreacion: number;
  estado: number;
}

export const ESTADO_LABELS: Record<number, string> = {
  1: 'Borrador',
  2: 'Enviado',
  3: 'Aceptado',
  4: 'Rechazado',
};

export const ESTADO_SEVERITIES: Record<number, string> = {
  1: 'secondary',
  2: 'info',
  3: 'success',
  4: 'danger',
};
