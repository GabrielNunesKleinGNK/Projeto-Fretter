import { EntityBase } from "./entityBase";

	export class OcorrenciaImportacao extends EntityBase {

		constructor(){
			super();
			this.id = 0;
		}
         transportadorId : Number;
         codigoOcorrencia : string;
         descricaoOcorrencia : string;
         codigoDanfe : string;
         codigoNotaFiscal : string;
         serieNotaFiscal : string;
         codigoPedido : string;
         codigoServico : string;
         numeroDocumentoRemetente : string;
         numeroDocumentoTransportador : string;
         latitude : string;
         longitude : string;
         cidade : string;
         uF : string;
    }
