import { ConciliacaoTipo } from "./conciliacaoTipo";
import { EntityBase } from "./entityBase";

export class ImportacaoArquivoTipoItem extends EntityBase {


	constructor() {
		super();
		this.id = 0;
	}
	importacaoArquivoTipoId: number;
	conciliacaoTipoId: number;
	conciliacaoTipo: ConciliacaoTipo = new ConciliacaoTipo();
	conciliacaoTipoNome: string;
}
