// Panel individual: 9 campos
export interface Panel {
  leve?:           number | null;
  totalDanyoLeve?: number | null;
  medio?:          number | null;
  totalDanyoMedio?: number | null;
  grave?:          number | null;
  totalDanyoGrave?: number | null;
  pintura?:        boolean | null;
  aluminio?:       boolean | null;
  total?:          number | null;
}

// DTO de lectura (lo que devuelve GET)
export interface PresupuestoDto {
  id: number;
  reparador?:   string | null;
  marca?:       string | null;
  modelo?:      string | null;
  matricula?:   string | null;
  precioTotal?: number | null;

  adiLeve?: number | null; adiTotalDanyoLeve?: number | null; adiMedio?: number | null;
  adiTotalDanyoMedio?: number | null; adiGrave?: number | null; adiTotalDanyoGrave?: number | null;
  adiPintura?: boolean | null; adiAluminio?: boolean | null; adiTotal?: number | null;

  addLeve?: number | null; addTotalDanyoLeve?: number | null; addMedio?: number | null;
  addTotalDanyoMedio?: number | null; addGrave?: number | null; addTotalDanyoGrave?: number | null;
  addPintura?: boolean | null; addAluminio?: boolean | null; addTotal?: number | null;

  atiLeve?: number | null; atiTotalDanyoLeve?: number | null; atiMedio?: number | null;
  atiTotalDanyoMedio?: number | null; atiGrave?: number | null; atiTotalDanyoGrave?: number | null;
  atiPintura?: boolean | null; atiAluminio?: boolean | null; atiTotal?: number | null;

  atdLeve?: number | null; atdTotalDanyoLeve?: number | null; atdMedio?: number | null;
  atdTotalDanyoMedio?: number | null; atdGrave?: number | null; atdTotalDanyoGrave?: number | null;
  atdPintura?: boolean | null; atdAluminio?: boolean | null; atdTotal?: number | null;

  pdiLeve?: number | null; pdiTotalDanyoLeve?: number | null; pdiMedio?: number | null;
  pdiTotalDanyoMedio?: number | null; pdiGrave?: number | null; pdiTotalDanyoGrave?: number | null;
  pdiPintura?: boolean | null; pdiAluminio?: boolean | null; pdiTotal?: number | null;

  pddLeve?: number | null; pddTotalDanyoLeve?: number | null; pddMedio?: number | null;
  pddTotalDanyoMedio?: number | null; pddGrave?: number | null; pddTotalDanyoGrave?: number | null;
  pddPintura?: boolean | null; pddAluminio?: boolean | null; pddTotal?: number | null;

  ptdLeve?: number | null; ptdTotalDanyoLeve?: number | null; ptdMedio?: number | null;
  ptdTotalDanyoMedio?: number | null; ptdGrave?: number | null; ptdTotalDanyoGrave?: number | null;
  ptdPintura?: boolean | null; ptdAluminio?: boolean | null; ptdTotal?: number | null;

  ptiLeve?: number | null; ptiTotalDanyoLeve?: number | null; ptiMedio?: number | null;
  ptiTotalDanyoMedio?: number | null; ptiGrave?: number | null; ptiTotalDanyoGrave?: number | null;
  ptiPintura?: boolean | null; ptiAluminio?: boolean | null; ptiTotal?: number | null;

  capoLeve?: number | null; capoTotalDanyoLeve?: number | null; capoMedio?: number | null;
  capoTotalDanyoMedio?: number | null; capoGrave?: number | null; capoTotalDanyoGrave?: number | null;
  capoPintura?: boolean | null; capoAluminio?: boolean | null; capoTotal?: number | null;

  techoLeve?: number | null; techoTotalDanyoLeve?: number | null; techoMedio?: number | null;
  techoTotalDanyoMedio?: number | null; techoGrave?: number | null; techoTotalDanyoGrave?: number | null;
  techoPintura?: boolean | null; techoAluminio?: boolean | null; techoTotal?: number | null;

  portonLeve?: number | null; portonTotalDanyoLeve?: number | null; portonMedio?: number | null;
  portonTotalDanyoMedio?: number | null; portonGrave?: number | null; portonTotalDanyoGrave?: number | null;
  portonPintura?: boolean | null; portonAluminio?: boolean | null; portonTotal?: number | null;

  miLeve?: number | null; miTotalDanyoLeve?: number | null; miMedio?: number | null;
  miTotalDanyoMedio?: number | null; miGrave?: number | null; miTotalDanyoGrave?: number | null;
  miPintura?: boolean | null; miAluminio?: boolean | null; miTotal?: number | null;

  mdLeve?: number | null; mdTotalDanyoLeve?: number | null; mdMedio?: number | null;
  mdTotalDanyoMedio?: number | null; mdGrave?: number | null; mdTotalDanyoGrave?: number | null;
  mdPintura?: boolean | null; mdAluminio?: boolean | null; mdTotal?: number | null;

  fechaCreacion?: number | null;
  descuento?:     number | null;
  observaciones?: string | null;
  desmontajes?:   number | null;
  estado?:        number | null;
  nombreCliente?: string | null;
  direccion?:     string | null;
  poblacion?:     string | null;
  nifCif?:        string | null;
  email?:         string | null;
  telefono?:      string | null;
  aseguradora?:   string | null;
  idPerito?:      number | null;
}

// DTO de escritura (POST/PUT) — mismo shape sin id
export type PresupuestoPayload = Omit<PresupuestoDto, 'id'>;

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
