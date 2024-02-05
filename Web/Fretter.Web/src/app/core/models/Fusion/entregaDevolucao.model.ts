import { EntityBase } from "../entityBase";

export class EntregaDevolucao extends EntityBase {

        constructor() {
                super();
                this.id = 0;
        }

        entregaId: Number;
        entregaTransporteTipoId: Number;
        codigoColeta: string;
        codigoRastreio: string;
        validade: Date;
        observacao: string;
        inclusao: Date;
        entregaDevolucaoStatus: Number;
        processado: boolean;
        status: any;
        entregaReversaId: Number;
}
