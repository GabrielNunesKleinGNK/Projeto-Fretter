import { EntityBase } from "./entityBase";

	export class EmpresaTransportador extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         empresaId : Number;
         transportadorId : Number;
         prioridade : Number;
         numeroDocumento : string;
         razaoSocial : string;
         nomeFantasia : string;
    }
