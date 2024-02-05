import { number, string } from "@amcharts/amcharts4/core";
import moment from "moment";
import { ConfiguracaoCteTipo } from "./configuracaoCteTipo";
import { EntityBase } from "./entityBase";
import { ToleranciaTipo } from "./toleranciaTipo";
import { Transportador } from "./transportador.model";
import { ContratoTransportadorRegra } from "./contratoTransportadorRegra";
import { ContratoTransportadorArquivoTipo } from "./contratoTransportadorArquivoTipo";

export class ContratoTransportador extends EntityBase {

  constructor() {
    super();
    this.id = 0;
    this.toleranciaSuperior = 0;
    this.toleranciaInferior = 0;
    this.permiteTolerancia = false;
    this.recotaPesoTransportador = false;
    this.conciliaEntregaFinalizada = false;
    this.faturaAutomatica = true;
    this.vigenciaInicial = moment().toDate();
    this.vigenciaFinal = moment().toDate();
    this.quantidadeTentativas = 100;
    this.taxaTentativaAdicional = 100;
    this.taxaRetornoRemetente = 100;
    this.contratoTransportadorRegra = new Array<ContratoTransportadorRegra>();
    this.contratoTransportadorArquivoTipo = new Array<ContratoTransportadorArquivoTipo>(); 
  }
  transportadorId: number;
  descricao: string;
  quantidadeTentativas: number = 100;
  taxaTentativaAdicional: number = 100;
  taxaRetornoRemetente: number = 100;
  vigenciaInicial: Date;
  vigenciaFinal: Date;
  dataCadastro: Date;
  transportadorCnpjId: number;
  transportadorCnpjCobrancaId: number;
  faturaCicloId: number;
  permiteTolerancia: boolean;
  faturaAutomatica: boolean;
  toleranciaSuperior: number;
  toleranciaInferior: number;
  toleranciaTipoId: number;
  microServicoId?: number;
  toleranciaTipo: ToleranciaTipo;
  transportador: Transportador;
  recotaPesoTransportador: boolean;
  transportadorCnpjListId: number[];
  transportadorCnpj: any;
  transportadorCnpjCobranca: any;
  conciliaEntregaFinalizada: boolean;
 // contratoTransportadorRegra: ContratoTransportadorRegra = new ContratoTransportadorRegra();
  contratoTransportadorRegra: Array<ContratoTransportadorRegra> = new Array<ContratoTransportadorRegra>();
  contratoTransportadorArquivoTipo : Array<ContratoTransportadorArquivoTipo> = new Array<ContratoTransportadorArquivoTipo>();
}
