import { Canal } from "./Fusion/Canal";
import { EntityBase } from "./entityBase";
import { RegraGrupoItem } from "./regraGrupoItem.model";
import { RegraItem } from "./regraItem.model";
import { Transportador } from "./transportador.model";
import { TransportadorCnpj } from "./transportadorCnpj.model";

export class AgendamentoRegra extends EntityBase{
    constructor(){
		super();
		this.id = 0;
	}

    empresaTransportadorId: number;
    regraStatusId: number;
    regraTipoId: number = 1;
    empresaId: number
    canalId: number;
    transportadorId: number;
    transportadorCnpjId: number;
    nome: string;
    definirVigencia: boolean;
    dataInicio: Date;
    dataTermino : Date;

    canal: Canal;
    transportador: Transportador;
    transportadorCnpj: TransportadorCnpj;

    regraItens = new Array<RegraItem>();

}