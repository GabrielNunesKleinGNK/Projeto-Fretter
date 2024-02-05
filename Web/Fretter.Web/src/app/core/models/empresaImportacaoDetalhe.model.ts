import { EntityBase } from "./entityBase";

	export class EmpresaImportacaoDetalhe extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}

          EmpresaImportacaoArquivoId: number
          EmpresaId: number;
          Token: string;
          DataCadastro: Date;
          UsuarioCadastro: number;
          Ativo: boolean;
    }
