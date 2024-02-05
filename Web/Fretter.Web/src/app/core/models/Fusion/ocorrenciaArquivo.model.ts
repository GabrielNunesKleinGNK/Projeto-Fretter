import { EntityBase } from "../entityBase";

export class OcorrenciaArquivo extends EntityBase {

        constructor() {
                super();
                this.id = 0;
        }

        TabelaArquivoStatusId : number;
        EmpresaId : number;
        NomeArquivo : string
        Url : string
        Retorno : string
        QtAdvertencia : number;
        QtErros : number;
        QtRegistros : number;
        PercentualAtualizacao : number;
        UltimaAtualizacao : Date;
        Usuario : string;            
}
