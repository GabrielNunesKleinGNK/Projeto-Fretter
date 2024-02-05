import { EntityBase } from "./entityBase";

	export class EntregaItem extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         entregaId : number;
         codigoProduto : string;
         produtoNome : string;
         produtoImagem : string;
         quantidade : Number;
         valorUnitario : Number;
         valorTotal : Number;
    }
