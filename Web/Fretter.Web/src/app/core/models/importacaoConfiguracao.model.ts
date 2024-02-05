import { EntityBase } from "./entityBase";

export class ImportacaoConfiguracao extends EntityBase {

     constructor() {
          super();
          this.id = 0;
     }
     importacaoConfiguracaoTipoId: Number;
     empresaId: Number;
     transportadorId: Number;
     importacaoArquivoTipoId: Number;
     diretorio: string;
     usuario: string;
     senha: string;
     outro: string;
     ultimaExecucao: Date;
     ultimoRetorno: string;
     sucesso: boolean;
     compactado: boolean;
     diretorioSucesso: string;
     diretorioErro: string;
}
