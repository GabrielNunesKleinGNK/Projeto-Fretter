export class FaturaConciliacaoIntegracao {

  constructor() { }
  
  empresaIntegracaoItemDetalheId: number;
  faturaConciliacaoId: number;
  faturaId: number;
  conciliacaoId: number;
  notaFiscal: string
  serie: string
  valorFrete: number;
  valorAdicional: number;
  dataEnvio: Date;
  dataProcessamento: Date;
  httpStatus: string;
  sucesso: boolean;
  enviado: boolean;
}
