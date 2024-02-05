import { EntityBase } from "../entityBase";

export class EmpresaIntegracaoItemDetalheFiltro extends EntityBase{
	constructor() {
		super();
	}

	transportadorId: number;
	canalId: number;
	descricao: string;
   	ocorrenciaId: number;
   	sigla:string;
   	dataOcorrencia: Date;
   	dataEnvioInicio: Date = new Date();
	dataEnvioFim: Date = new Date();
	sucesso: boolean = false;

	pagina: number;
	paginaLimite: number;
}
