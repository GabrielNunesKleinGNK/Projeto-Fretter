import { EntityBase } from "./entityBase";
import { FaturaAcao } from "./faturaAcao.model";

export class FaturaStatusAcao extends EntityBase {

        constructor() {
                super();
                this.id = 0;
        }

        faturaStatusId: Number;
        faturaAcaoId: Number;
        faturaStatusResultadoId: Number;
        informaMotivo: boolean;
        visivel: boolean;

        faturaAcao : FaturaAcao;
}
