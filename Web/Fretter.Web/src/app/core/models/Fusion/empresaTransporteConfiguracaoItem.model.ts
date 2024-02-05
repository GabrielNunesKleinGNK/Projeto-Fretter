import { EntityBase } from "../entityBase";
import { EmpresaTransporteTipoItem } from "./empresaTransporteTipoItem.model";

export class EmpresaTransporteConfiguracaoItem extends EntityBase{
	constructor() {
		super();
		this.id = 0;
	}


	id								: number;
	empresaTransporteConfiguracaoId : number;
	codigoServico					: string;
	codigoServicoCategoria			: string;
	codigoIntegracao				: string;
	nome							: string;
	dataCadastroServico				: Date;
	vigenciaInicial					: Date;
	vigenciaFinal					: Date;
	dataAtualizacao					: Date;
}
