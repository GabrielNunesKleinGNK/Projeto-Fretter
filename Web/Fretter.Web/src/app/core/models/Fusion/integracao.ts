import { EntityBase } from "../entityBase";

export class Integracao extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
    this.paralelo = 1;
    this.producao = true;
    this.dataCadastro = new Date();
    this.verbo = 'POST';
    this.registros = 1;
    this.lote = false;
    this.integracaoTipoId = 1;
    }
    
    integracaoTipoId : number;
    empresaeIntegracaoId : number;
    url : string;
    verbo : string;
    lote : boolean;
    layoutHeader : string;
    layout : string;
    registros : number;
    paralelo : number;
    producao : boolean;
    envioBody : boolean;
    enviaOcorrenciaHistorico : boolean;
    envioConfigId : number;
    integracaoGatilho : boolean;
}
