
import { EntityBase } from "./entityBase";

	export class Cliente extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         nome : string;
         numeroDocumento : string;
         email : string;
         telefone : string;
         celular : string;
         senhaAcesso : string;
         clienteAutenticacaoTipoId : Number;
    }
