import { Usuario } from "./usuario";

export class EntityBase {
	id: number;
	ativo: boolean;
	dataCadastro: Date;
	dataAlteracao: Date;
	usuarioCadastro: Usuario;
	usuarioAlteracao: Usuario;
	constructor() {
		this.ativo = true;
	}
}
