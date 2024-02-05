import { BehaviorSubject, Observable, Subject, from, throwError } from 'rxjs';
import { map, catchError, tap, switchMap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { AuthService } from 'ngx-auth';
import { TokenStorage } from './token-storage.service';
import { UtilsService } from '../services/utils.service';
import { AccessData } from './access-data';
import { Credential } from './credential';
import { environment } from "../../../environments/environment";
import { AppConfig } from '../models/app.config';

@Injectable()
export class AuthenticationService implements AuthService {
	API_ENDPOINT_LOGIN = 'connect/token';
	API_ENDPOINT_REFRESH = 'refresh';
	API_ENDPOINT_REGISTER = 'register';
	APP_CONFIG = environment.authSettings;

	public onCredentialUpdated$: Subject<AccessData>;

	constructor(
		private http: HttpClient,
		private tokenStorage: TokenStorage,
		private util: UtilsService,
		private config: AppConfig
	) {
		this.onCredentialUpdated$ = new Subject();
	}

	/**
	 * Check, if user already authorized.
	 * @description Should return Observable with true or false values
	 * @returns {Observable<boolean>}
	 * @memberOf AuthService
	 */
	public isAuthorized(): Observable<boolean> {
		return this.tokenStorage.getAccessToken().pipe(map(token => !!token));
	}

	/**
	 * Get access token
	 * @description Should return access token in Observable from e.g. localStorage
	 * @returns {Observable<string>}
	 */
	public getAccessToken(): Observable<string> {
		return this.tokenStorage.getAccessToken();
	}

	/**
	 * Get user roles
	 * @returns {Observable<any>}
	 */
	public getUserRoles(): Observable<any> {
		return this.tokenStorage.getUserRoles();
	}

	/**
	 * Function, that should perform refresh token verifyTokenRequest
	 * @description Should be successfully completed so interceptor
	 * can execute pending requests or retry original one
	 * @returns {Observable<any>}
	 */
	public refreshToken(): Observable<AccessData> {
		return this.tokenStorage.getRefreshToken().pipe(
			switchMap((refreshToken: string) => {
				return this.http.get<AccessData>(this.config.baseUrl + this.API_ENDPOINT_REFRESH + '?' + this.util.urlParam(refreshToken));
			}),
			tap(this.saveAccessData.bind(this)),
			catchError(err => {
				this.logout();
				return throwError(err);
			})
		);
	}

	/**
	 * Function, checks response of failed request to determine,
	 * whether token be refreshed or not.
	 * @description Essentialy checks status
	 * @param {Response} response
	 * @returns {boolean}
	 */
	public refreshShouldHappen(response: HttpErrorResponse): boolean {
		return response.status === 401;
	}

	/**
	 * Verify that outgoing request is refresh-token,
	 * so interceptor won't intercept this request
	 * @param {string} url
	 * @returns {boolean}
	 */
	public verifyTokenRequest(url: string): boolean {
		return url.endsWith(this.API_ENDPOINT_REFRESH);
	}

	/**
	 * Submit login request
	 * @param {Credential} credential
	 * @returns {Observable<any>}
	 */
	public login(credential: Credential): Observable<any> {
		// Expecting response from API
		// {"id":1,"username":"admin","password":"demo","email":"admin@demo.com","accessToken":"access-token-0.022563452858263444","refreshToken":"access-token-0.9348573301432961","roles":["ADMIN"],"pic":"./assets/app/media/img/users/user4.jpg","fullname":"Mark Andre"}

		const httpOptions = {
			headers: new HttpHeaders({
				'Content-Type': 'application/x-www-form-urlencoded'
			})
		};

		//let body = this.util.urlParam(credential);
		let body = this.setBody(credential.username, credential.password)
		return this.http.post<AccessData>(this.config.baseUrl + '/' + this.API_ENDPOINT_LOGIN, body, httpOptions).pipe(
			map((result: any) => {
				if (result instanceof Array) {
					return result.pop();
				}
				return result;
			}),
			tap(this.saveAccessData.bind(this)),
			catchError(this.handleError('login', []))
		);
	}

	public clearLocalStorage(){
		this.tokenStorage.clear();
	}

	public refreshTokenImpersonate(refreshToken: string): Observable<AccessData> {
		const httpOptions = {
			headers: new HttpHeaders({
				'Content-Type': 'application/x-www-form-urlencoded'
			})
		};

		let body = this.setBodyRefreshToken(refreshToken);
		return this.http.post<AccessData>(this.config.baseUrl + '/' + this.API_ENDPOINT_LOGIN, body, httpOptions).pipe(
			map((result: any) => {
				if (result instanceof Array) {
					return result.pop();
				}
				return result;
			}),
			tap(this.saveAccessData.bind(this)),
			catchError(this.handleError('login', []))
		);
	}

	private setBody(username, password): String {
		return "username=" + username + "&password=" + password + "&email=&client_id=" + this.APP_CONFIG.client_id + "&client_secret=" + this.APP_CONFIG.client_secret + "&scope=" + this.APP_CONFIG.scope + "&grant_type=" + this.APP_CONFIG.grant_type;
	}

	private setBodyRefreshToken(refresh_token: String) {
		return "refresh_token=" + refresh_token + "&email=&client_id=" + this.APP_CONFIG.client_id + "&client_secret=" + this.APP_CONFIG.client_secret + "&scope=" + this.APP_CONFIG.scope + "&grant_type=" + this.APP_CONFIG.grant_type_refreshToken;
	}

	/**
	 * Handle Http operation that failed.
	 * Let the app continue.
	 * @param operation - name of the operation that failed
	 * @param result - optional value to return as the observable result
	 */
	private handleError<T>(operation = 'operation', result?: any) {
		return (error: any): Observable<any> => {
			// TODO: send the error to remote logging infrastructure
			console.error(error); // log to console instead

			// Let the app keep running by returning an empty result.
			return from(result);
		};
	}

	/**
	 * Logout
	 */
	public logout(refresh?: boolean): void {
		this.tokenStorage.clear();
		if (refresh) {
			location.reload();
		}
	}

	/**
	 * Save access data in the storage
	 * @private
	 * @param {AccessData} data
	 */
	saveAccessData(accessData: AccessData) {
		if (typeof accessData !== 'undefined') {
			this.tokenStorage
				.setAccessToken(accessData.access_token)
				.setRefreshToken(accessData.refresh_token)
				.setUserRoles(accessData.roles);
			this.onCredentialUpdated$.next(accessData);
		}
	}

	/**
		 * Save userMenu in the storage
		 * @param {AccessData} data
		 */
	async saveUserMenu(){
		await this.http.get<any>(this.config.baseUrl + "/api/SistemaMenu/Menus").toPromise()
			.then(data => {
				if (data !== 'undefined')
					this.tokenStorage.setUserMenu(data.menuItens);
				this.tokenStorage.setPathPermission(data.paths);
			});
	}
	

	/**
	 * Submit registration request
	 * @param {Credential} credential
	 * @returns {Observable<any>}
	 */
	public register(credential: Credential): Observable<any> {
		// dummy token creation
		credential = Object.assign({}, credential, {
			accessToken: 'access-token-' + Math.random(),
			refreshToken: 'access-token-' + Math.random(),
			roles: ['USER'],
		});
		return this.http.post(this.config.baseUrl + this.API_ENDPOINT_REGISTER, credential)
			.pipe(catchError(this.handleError('register', []))
			);
	}

	/**
	 * Submit forgot password request
	 * @param {Credential} credential
	 * @returns {Observable<any>}
	 */
	public requestPassword(credential: Credential): Observable<any> {
		return this.http.get(this.config.baseUrl + this.API_ENDPOINT_LOGIN + '?' + this.util.urlParam(credential))
			.pipe(catchError(this.handleError('forgot-password', []))
			);
	}

}
