import { FaturaConciliacaoIntegracao } from "./faturaConciliacaoIntegracao.model";

export class ResultadoFaturaConciliacaoIntegracao {
    qtdeTotal: number;
    qtdeSucesso: number;
    qtdeInsucesso: number;
    qtdeNaoDefinida: number;
    
    valorTotal: number;
    dataUltimoEnvio: Date;

    integracoes: Array<FaturaConciliacaoIntegracao>;
    integracoesSucesso: Array<FaturaConciliacaoIntegracao>;
    integracoesInsucesso: Array<FaturaConciliacaoIntegracao>;
    integracoesNaoDefinida: Array<FaturaConciliacaoIntegracao>;
}