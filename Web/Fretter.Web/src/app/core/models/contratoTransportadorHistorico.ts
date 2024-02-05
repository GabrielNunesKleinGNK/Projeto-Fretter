import { number, string } from "@amcharts/amcharts4/core";
import moment from "moment";
import { ConfiguracaoCteTipo } from "./configuracaoCteTipo";
import { EntityBase } from "./entityBase";
import { ToleranciaTipo } from "./toleranciaTipo";
import { Transportador } from "./transportador.model";

export class ContratoTransportadorHistorico extends EntityBase {
	
	
	constructor(){
      super();
      this.id = 0;
      this.toleranciaSuperior = 0;
      this.toleranciaInferior = 0;
      this.permiteTolerancia = false;
      this.vigenciaInicial = moment().toDate();
      this.vigenciaFinal = moment().toDate();
    }
    transportadorId: number;  
    descricao: string;
    quantidadeTentativas: number;
    taxaTentativaAdicional: number;
    taxaRetornoRemetente: number;
    vigenciaInicial: Date; 
    vigenciaFinal: Date;     
    dataCadastro: Date;
    transportadorCnpjId: number;  
    transportadorCnpjCobrancaId: number;
    faturaCicloId: number;  
    permiteTolerancia: boolean;
    toleranciaSuperior: number;
    toleranciaInferior: number;
    toleranciaTipoId: number;
    microServicoId?: number;
    toleranciaTipo: ToleranciaTipo;
    transportador: Transportador;

    transportadorCnpj: any;
    transportadorCnpjCobranca: any;
    faturaCiclo: any;
}
