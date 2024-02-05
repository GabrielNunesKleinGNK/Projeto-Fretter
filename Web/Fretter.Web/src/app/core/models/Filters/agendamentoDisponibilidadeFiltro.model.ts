export class AgendamentoDisponibilidadeFiltro {
    constructor() { 
        this.quantidadeItens = 1;
        this.pagina = 1;
    }

    canalId: number;
    cep: string;
    quantidadeItens: number = 1;
    pagina: number = 1;
}