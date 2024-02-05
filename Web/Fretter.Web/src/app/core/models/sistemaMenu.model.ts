import { EntityBase } from "./entityBase";

	export class SistemaMenu extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
		 descricao : string;
		 icone : string;
		 rota : string;
    }
