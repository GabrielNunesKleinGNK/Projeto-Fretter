import { ConfiguracaoCteTipo } from "./configuracaoCteTipo";
import { EntityBase } from "./entityBase";
import { TransportadorCnpj } from "./Fusion/transportadorCnpj";

export class ConfiguracaoCteTransportador extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
	}
    nome: string;
    alias : string;
    configuracaoCteTipoId: number;
    transportadorCnpjId: number;
    transportadorId: number;
    transportadorCnpj: TransportadorCnpj;
    configuracaoCteTipo: ConfiguracaoCteTipo;
}
