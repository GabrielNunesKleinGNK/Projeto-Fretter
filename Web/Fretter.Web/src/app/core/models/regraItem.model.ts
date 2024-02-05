import { EntityBase } from "./entityBase";
import { RegraGrupoItem } from "./regraGrupoItem.model";

export class RegraItem extends EntityBase {
    regraId: number;
    regraGrupoItemId: number;
    regraTipoItemId: number;
    regraTipoOperadorId: number;
    valor: string
    valorInicial: string;
    valorFinal: string;

    regraGrupoItem: RegraGrupoItem = new RegraGrupoItem();
}