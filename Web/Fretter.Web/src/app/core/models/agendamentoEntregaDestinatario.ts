import { EntityBase } from "./entityBase";

export class AgendamentoEntregaDestinatario extends EntityBase {

	constructor(){
		super();
		this.id = 0;
	}
       
    nome :  string;
    cpfCnpj : string;
    inscricaoEstadual : string;
    documentoHash : string;
    cep : string;   
    logradouro : string;
    numero : string;
    complemento : string;
    pontoReferencia : string;
    bairro : string;     
    cidade : string;    
    uf : string;       
    email : string;     
    telefone : string;   
    celular : string;    
    whatsapp : boolean;   
}