
import { Empresa } from "../empresa.model";
import { EntityBase } from "../entityBase";

export class Canal extends EntityBase {
	
	constructor(){
		super();
		this.id = 0;
    }
    inclusao: Date;
    razaoSocial: string;
    nomeFantasia: string;
    cnpj: string;
    canalNome: string;
    segmentoId: number;
    origemImportacaoId: number;
    inscricaoEstadual: string;
    empresaId: number;
    dataCadastro: Date;

}
