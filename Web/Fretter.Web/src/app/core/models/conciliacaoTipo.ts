import { EntityBase } from "./entityBase";

export class ConciliacaoTipo extends EntityBase {

  constructor() {
    super();
    this.id = 0;        
    this.ativo = true;
  }
  
  nome: string;
  descricao: string;  
}
