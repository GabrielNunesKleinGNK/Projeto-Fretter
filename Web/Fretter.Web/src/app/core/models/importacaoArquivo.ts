import { EntityBase } from "./entityBase";
import { ImportacaoArquivoCritica } from "./importacaoArquivoCritica";
import { ImportacaoArquivoStatus } from "./importacaoArquivoStatus";
import { ImportacaoArquivoTipo } from "./importacaoArquivoTipo";

export class ImportacaoArquivo extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}
    nome: string;
    empresaId : number;
    identificador : string;
    importacaoArquivoStatusId : number;
    importacaoArquivoTipoId: number;
    diretorio : string;
    importacaoArquivoStatus : ImportacaoArquivoStatus;
    importacaoArquivoTipo: ImportacaoArquivoTipo;

    arquivo: string;

    criticas: Array<ImportacaoArquivoCritica>
}
