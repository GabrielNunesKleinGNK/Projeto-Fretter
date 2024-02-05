import { EntityBase } from "../entityBase";

export class CanalVenda {
	
	constructor(){
    }
    
    id: number;
    descricaoCanalVenda: string;
    default: boolean;
    empresaId: number;
    descricaoCanalVendaUnico: string;
    defaultunico: number;
    canalVendaCod: string;
    ultAtualizacaoProduto : Date;
    tipoIntegracaoId: number;
    embalagemUnicaMF: boolean;
    origemImportacaoId: number;
}
