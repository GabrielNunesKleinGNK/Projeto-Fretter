export class LogCotacaoFreteFiltro {
    constructor() {
        this.periodoId = 1;
        this.dataInicio = new Date();
        this.dataTermino = new Date();
    }
    cep: string;
    empresaId: number;
    instancia: string;
    peso: string;
    processName: string;
    message: string;
    lineNumber: string;
    exceptionType: string;
    periodoId: number;
    dataInicio: Date;
    dataTermino: Date;
    periodos: any[] = [
        { id: 1, nome: "Hoje" },
        { id: 2, nome: "Ontem" },
        { id: 3, nome: "Semana Atual" },
        { id: 4, nome: "Semana Passada" },
        { id: 5, nome: "Mes Atual" },
        { id: 6, nome: "Mes Passado" },
        { id: 7, nome: "Trimestre" },
        { id: 8, nome: "Semestre" },
        { id: 9, nome: "Ano Atual" },
        { id: 10, nome: "12 Meses" },
        { id: 11, nome: "Custom" },
    ];
}
