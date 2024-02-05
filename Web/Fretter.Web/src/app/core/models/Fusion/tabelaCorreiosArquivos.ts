import { NumericDictionaryIteratee } from "lodash";
import { EntityBase } from "../entityBase";
import { Integracao } from "./integracao";

export class TabelaCorreiosArquivos extends EntityBase {
	constructor(){
		super();
		this.id = 0;
    }
    
    TabelaArquivoStatusId : number;
    NomeArquivo : string;
    Url : string;
    Criacao: Date;
    ImportacaoDados : Date
    AtualizacaoTabelas : Date
    Erro : string;
}
