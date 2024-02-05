import { EntityBase } from "./entityBase";

	export class EmpresaUsuario extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         login : string;
         email : string;
         nome : string;
         senha : string;
    }
