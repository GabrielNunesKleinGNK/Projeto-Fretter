
import { NumericDictionaryIteratee } from "lodash";
import { EntityBase } from "../entityBase";
import { Integracao } from "./integracao";

export class EmpresaIntegracao extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
    this.listaIntegracoes = new Array<Integracao>();
    this.visualizar = false;
    }

    empresaId: number;
    canalVendaId: number;
    urlBase: string;
    urlToken: string;
    apiKey: string;
    usuario: string;
    senha: string;
    clientId: string;
    clientSecret: string;
    clientScope: string;
    entregaOrigemImportacaoId: number;

    listaIntegracoes: Array<Integracao>;

    visualizar : boolean;
}
