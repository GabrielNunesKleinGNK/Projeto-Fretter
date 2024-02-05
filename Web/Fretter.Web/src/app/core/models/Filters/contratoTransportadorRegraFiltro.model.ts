import { EntityBase } from "../entityBase";

export class ContratoTransportadorRegraFiltro extends EntityBase{
	constructor() {
		super();
	}

	transportadorId: number;	
   	ocorrenciaEmpresaItemId: number;
   	conciliacaoTipoId: number;	
}
