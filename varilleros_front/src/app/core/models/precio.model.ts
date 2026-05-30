export interface PrecioDto {
  numeroabolladuras: number;
  aletaLeve?:    number | null;
  aletaMedio?:   number | null;
  aletaGrave?:   number | null;
  puertaLeve?:   number | null;
  puertaMedio?:  number | null;
  puertaGrave?:  number | null;
  techoLeve?:    number | null;
  techoMedio?:   number | null;
  techoGrave?:   number | null;
  capoLeve?:     number | null;
  capoMedio?:    number | null;
  capoGrave?:    number | null;
  portonLeve?:   number | null;
  portonMedio?:  number | null;
  portonGrave?:  number | null;
  montanteLeve?:  number | null;
  montanteMedio?: number | null;
  montanteGrave?: number | null;
}

export type UpdatePrecioDto = PrecioDto;
export type CreatePrecioDto = Pick<PrecioDto, 'numeroabolladuras'>;
