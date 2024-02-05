import { EntityBase } from "../entityBase";

export class EntregaAberta extends EntityBase {

        constructor() {
                super();
                this.id = 0;
        }

        codigoEntrega: string;
        canalId: number;
        canal: string;
        transportadorId: number;
        transportador: string;
        nota: string;
        serie: string;
        diasSemAlteracao: number;
        dataUltimaOcorrencia: Date;
        ultimaOcorrencia: string;

        total: number;
}
