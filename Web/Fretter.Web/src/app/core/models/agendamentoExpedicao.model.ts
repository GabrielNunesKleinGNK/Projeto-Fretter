import { EntityBase } from "./entityBase";
import { Canal } from "./Fusion/Canal";
import { Transportador } from "./transportador.model";

export class AgendamentoExpedicao extends EntityBase {

	constructor(){
		super();
		this.id = 0;
	}

    empresaId: number;
    canalId: number;
    transportadorId: number;
    transportadorCnpjId: number;
    expedicaoAutomatica: boolean;
    prazoComercial: number;
    
    canal: Canal;
    transportador: Transportador;
    transportadorCnpj: any;
}