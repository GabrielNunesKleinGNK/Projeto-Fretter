import { EntityBase } from "./entityBase";

	export class OcorrenciaParametro extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         ocorrenciaEmpresaId : Number;
         ocorrenciaTransportadorId : Number;
         visivel : boolean;
    }
