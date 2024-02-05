import { EntityBase } from "./entityBase";

	export class Usuario extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         nome : string;
         email : string;
         login : string;
         senha : string;
         usuarioTipoId : Number;
    }
