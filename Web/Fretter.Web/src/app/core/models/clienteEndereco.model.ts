import { EntityBase } from "./entityBase";

	export class ClienteEndereco extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         clienteId : Number;
         origem : string;
         logradouro : string;
         numero : string;
         bairro : string;
         cidade : string;
         codigoIBGE : Number;
         uF : string;
         cEP : string;
         complemento : string;
    }
