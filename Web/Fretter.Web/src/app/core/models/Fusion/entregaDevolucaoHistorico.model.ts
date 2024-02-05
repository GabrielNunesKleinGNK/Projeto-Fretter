import { EntityBase } from "../entityBase";

export class EntregaDevolucaoHistorico extends EntityBase {

        constructor(){
                super();
                this.id = 0;
        }

        entregaDevolucaoId : Number;
        codigoColeta : string;
        codigoRastreio : string;
        validade : Date;
        mensagem : string;
        retorno : string;
        entregaDevolucaoStatusAnteriorId : Number;
        entregaDevolucaoStatusAtualId : Number;
        inclusao : Date;
        xmlRetorno : string;
}
