
import { NumericDictionaryIteratee } from "lodash";
import { EntityBase } from "../entityBase";

export class EmpresaIntegracaoItemDetalhe extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
    }

    empresaIntegracaoItemId : number
    codigoIntegracao  : number
    chave : string
    requestURL : string
    jsonEnvio : string
    jsonBody : string
    jsonRetorno : string
    httpTempo : number
    httpStatus : string
    httpResponse : string 
    sucesso : boolean
    pendenteProcessamentoRetorno : boolean
    processadoRetornoSucesso : boolean
    mensagemRetorno : string
    httpStatusCode : number
    ativo : boolean
}
