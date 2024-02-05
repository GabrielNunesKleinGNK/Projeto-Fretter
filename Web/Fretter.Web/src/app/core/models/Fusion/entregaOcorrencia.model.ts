import { EntityBase } from "../entityBase";

export class EntregaOcorrencia extends EntityBase {

        constructor() {
                super();
                this.id = 0;
                this.listaEntregas = new Array<number>();
        }

        listaEntregas: Array<number>;
        entregaId: number;
        codigoEntrega: string;
        ocorrenciaId: number;
        ocorrencia: string;
        dataOcorrencia: Date;
        transportadorId: number;
        complementar: string;
        sigla: string;
        finalizar : boolean;
        dominio: string;
        cidade : string;
        uf: string;
        latitude: string;
        longitude: string;
}
