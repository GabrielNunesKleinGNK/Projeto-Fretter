import { Canal } from "./Fusion/Canal";
import { AgendamentoEntregaDestinatario } from "./agendamentoEntregaDestinatario";
import { AgendamentoEntregaProduto } from "./agendamentoEntregaProduto";
import { EntityBase } from "./entityBase";

export class AgendamentoEntrega extends EntityBase {

	constructor(){
		super();
		this.id = 0;
	}
    
    idCanal 	: number;	
    idRegiaoCEPCapacidade : number = 0;
    idTransportador : number;
    idTransportadorCnpj : number;
    cdNotaFiscal : string = '';
    cdSerie : string = '';
    cdSro : string = '';
    cdEntrega : string = '';
    cdPedido : string = '';
    cdCepOrigem : string = '';
    cdCepDestino : string = '';
    cdProtocolo : string = '';
    nrPrazoTransportador : number = 0;
    vlQuantidade : number = 1;
    dtAgendamento : Date = new Date();
    dsObservacao : string = '';

    destinatario : AgendamentoEntregaDestinatario = new AgendamentoEntregaDestinatario();
    //destinatarios : Array<AgendamentoEntregaDestinatario> = new Array<AgendamentoEntregaDestinatario>();
    produtos : Array<AgendamentoEntregaProduto> = Array<AgendamentoEntregaProduto>();
    canal : Canal;
    menuFreteRegiaoCepCapacidade: any;

    //dataSelecionada: string;
    periodoSelecionado: string;
}