import { EntityBase } from "./entityBase";
import { ImportacaoArquivoTipoItem } from "./importacaoArquivoTipoItem";

export class ContratoTransportadorArquivoTipo extends EntityBase {

  constructor() {
    super();
    this.id = 0;
    this.importacaoArquivoTipoItemId = 0;
    this.transportadorId = 0;
    this.ativo = true;
    this.importacaoArquivoTipoItem = new ImportacaoArquivoTipoItem();
  }
	
  empresaId	: number;				
  transportadorId	: number;				
  importacaoArquivoTipoItemId : number;		
  alias: string;
  aliasList: Array<string> = new Array<string>();

  importacaoArquivoTipoItem: ImportacaoArquivoTipoItem  = new ImportacaoArquivoTipoItem();

  tipoArquivo : string;
  tipoCobranca : string;
}
