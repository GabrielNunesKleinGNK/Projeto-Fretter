import { EntityBase } from "./entityBase";

export class ImportacaoFatura extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}
    conciliacaoId : number;
    entregaId : number;
    conciliacaoTipo: string;
    codigoPedido: string;
    valorCustoFrete : number;
    valorCustoReal : number;
    valorFreteDoccob : number;
    transportadorId : number;
    transportador: string;    
    notaFiscal : number;
    serie : number;
    statusConciliacao: string;
    habilitado: boolean;
    selecionado: boolean;
    dataEmissao: Date;
    dataCadastro: Date;
    diretorio : string;
    arquivo: string;
}