import { EntityBase } from "./entityBase";

export class ContratoTransportadorRegra extends EntityBase {

  constructor() {
    super();
    this.id = 0;    
    this.valor = 1;
    this.transportadorId = 0;
    this.ativo = true;
  }

  ocorrenciasListId: Array<number>;
  ocorrenciaId: number;
  ocorrencia: string;
  tipoCondicao: number;
  operacao: number;
  valor: number;
  transportadorId: number;
  conciliacaoTipoId : number;  
}
