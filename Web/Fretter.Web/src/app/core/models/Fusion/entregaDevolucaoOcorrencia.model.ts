import { EntityBase } from "../entityBase";

export class EntregaDevolucaoOcorrencia extends EntityBase {

        constructor(){
                super();
                this.id = 0;
        }

        entregaDevolucao : Number;
        ocorrenciaEmpresaItem : Number;
        codigoIntegracao : string;
        ocorrencia : string;
        observacao : string;
        sigla : string;
        dataOcorrencia : Date;
}
