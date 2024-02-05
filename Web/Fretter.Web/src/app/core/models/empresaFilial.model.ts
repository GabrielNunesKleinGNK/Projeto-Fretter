import { EntityBase } from "./entityBase";

	export class EmpresaFilial extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         empresaId : Number;
         matriz : boolean;
         numeroDocumento : string;
         razaoSocial : string;
         nomeFantasia : string;
         iE : string;
         uF : string;
    }
