import { EntityBase } from "../entityBase";

export class EntregaDevolucaoStatusAcao extends EntityBase {

        constructor() {
                super();
                this.id = 0;
        }

        entregaTransporteTipoId: Number;
        entregaDevolucaoStatusId: Number;
        entregaDevolucaoAcaoId: Number;
        entregaDevolucaoResultadoId: Number;
        informaMotivo: boolean;
        visivel: boolean;
}
