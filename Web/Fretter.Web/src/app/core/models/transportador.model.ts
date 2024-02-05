import { EntityBase } from "./entityBase";

	export class Transportador extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         numeroDocumento : string;
         razaoSocial : string;
         nomeFantasia : string;
    }
