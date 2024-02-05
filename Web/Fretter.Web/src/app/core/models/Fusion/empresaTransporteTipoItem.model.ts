import { EntityBase } from "../entityBase";
import { Transportador } from "../transportador.model";
import { EmpresaTransporteConfiguracao } from "./empresaTransporteConfiguracao.model";
import { EmpresaTransporteTipo } from "./empresaTransporteTipo.model";

export class EmpresaTransporteTipoItem extends EntityBase {
	constructor() {
		super();
		this.id = 0;
		this.empresaTransporteConfiguracoes = new Array<EmpresaTransporteConfiguracao>();
		this.empresaTransporteTipoId = 0;
	}

	empresaTransporteTipoId: number;
	transportadorId: number;
	url: string;
	alias: string;
	codigoIntegracao: String;

	empresaTransporteConfiguracoes: Array<EmpresaTransporteConfiguracao>;
	empresaTransporteTipo: EmpresaTransporteTipo;
	transportador: Transportador;
}
