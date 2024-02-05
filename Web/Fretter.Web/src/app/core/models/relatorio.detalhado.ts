import { EntityBase } from "./entityBase";

export class RelatorioDetalhado extends EntityBase {
	usuarioId:number;
	usuarioNome: string;
	clienteId:number;
	clienteNome : string;
	dataEntrada: Date;
	dataSaida: Date;
	lancamentoEntrada: number;
	lancamentoSaida: number;
	faturado: boolean;
	notaFiscalId: Number;
}
