import { EntityBase } from "./entityBase";

	export class EmpresaImportacao extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}

         Nome: string;
         Descricao: string;
         EmpresaId: number;
         ArquivoURL: string;
         QuantidadeEmpresa: number;
         Processado: boolean;
         Sucesso: boolean;
         DataCadastro: Date;
         UsuarioCadastro: number;
         Ativo: boolean;
    }
