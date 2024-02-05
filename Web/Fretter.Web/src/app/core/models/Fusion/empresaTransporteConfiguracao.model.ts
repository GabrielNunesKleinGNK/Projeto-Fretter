import { EntityBase } from "../entityBase";
import { EmpresaTransporteTipoItem } from "./empresaTransporteTipoItem.model";

export class EmpresaTransporteConfiguracao extends EntityBase {
	constructor() {
		super();
		this.id = 0;
		this.empresaTransporteTipoItemId = 0;
		this.valido = false;
		this.producao = false;
		this.diasValidade = 0;
		this.empresaTransporteTipoItem = new EmpresaTransporteTipoItem();
	}

	empresaTransporteTipoItem: EmpresaTransporteTipoItem;
	empresaTransporteTipoItemId?: number;
	empresaId: number;
	codigoContrato: string;
	codigoIntegracao: string;
	codigoCartao: string;
	usuario: string;
	senha: string;
	vigenciaInicial: Date;
	vigenciaFinal: Date;
	diasValidade: number;
	retornoValidacao: string;
	dataCadastro: Date;
	dataValidacao: Date;
	valido: boolean;
	producao: boolean;
}
