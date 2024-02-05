import { EntityBase } from "./entityBase";

export class Usuario extends EntityBase {
	
	
	constructor(){
		super();
		this.id = 0;
		this.alterarSenha = true;
	}
	nome: string;
	login: string;
	email: string;
	senha: string;
	alterarSenha: boolean;
	avatar: string;
	administrador: boolean;
	senhaAlterada: boolean;
	usuarioTipoId : number;
	telefone : string;
	cargo: string;
	superior: number;
	bloqueado: boolean;
	regionalId: number;
	grupoPosVendaId : number;
}
