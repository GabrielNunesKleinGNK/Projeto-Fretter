import { EntityBase } from "./entityBase";

export class DisponibilidadeEntrega extends EntityBase {

	constructor(){
		super();
		this.id = 0;
        this.data = new Date();
        this.manha = 0;
        this.tarde = 0;
        this.noite = 0;
	}
    
    data: Date;
    dataCompleta: string;
    manha: number;
    tarde: number;
    noite: number;

    desabilitaManha: boolean;
    desabilitaTarde: boolean;
    desabilitaNoite: boolean;

    transportadorId: number;
    transportadorCnpjId: number;
}