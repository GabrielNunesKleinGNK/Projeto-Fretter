import { EntityBase } from "./entityBase";

	export class OcorrenciaTransportador extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         transportadorId : Number;
         codigo : string;
         descricao : string;
         ocorrenciaTipoId : Number;
    }
