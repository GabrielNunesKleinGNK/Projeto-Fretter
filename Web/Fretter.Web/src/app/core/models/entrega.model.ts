import { EntityBase } from "./entityBase";

	export class Entrega extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         empresaId : Number;
         empresaFilialId : Number;
         clienteId : Number;
         transportadorId : Number;
         codigoIntegracao : string;
         codigoPedido : string;
         codigoPedidoExterno : string;
         dataPostagem : Date;
         dataEmissaoNotaFiscal : Date;
         dataPrevisaoEntrega : Date;
         dataEntrega : Date;
         logradouro : string;
         numero : string;
         bairro : string;
         cidade : string;
         codigoIBGE : Number;
         uF : string;
         cEP : string;
         telefone : string;
         email : string;
         numeroDocumentoFilial : string;
         numeroDocumentoTransportador : string;
         numeroDocumentoCliente : string;
         jsonEntrega : string;
    }
