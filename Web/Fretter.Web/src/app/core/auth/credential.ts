export interface Credential {
	username: string;
	password: string;
	scope: string;
	client_id: string;
	client_secret: string;
	grant_type: string;
	grant_type_refreshToken: string;
}
