import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
    HttpInterceptor,
    HttpRequest,
    HttpResponse,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AuthenticationService } from '../auth/authentication.service';
import { AlertService } from '../services/alert.service';

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
    constructor(private router: Router, private authService: AuthenticationService, private _alertService: AlertService) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token: string = localStorage.getItem('accessToken');

        if (token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
        }

        if (!request.headers.has('Content-Type') && !request.url.includes("upload")) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }

        if (!request.url.includes("upload"))
            request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
        else
            request = request.clone();

        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    // this.errorDialogService.openDialog(event);
                }
                return event;
            }),
            catchError((error: HttpErrorResponse) => {
                let data = {};
                if (error.status == 401) {
                    this.authService.logout(true);
                }
                else { //if(error.status != 400){
                    var errroMessage: string;

                    if (error && error.error && error.error.errors)
                        errroMessage = error.error.errors.map(x => x.message).join("/n");
                    else if (error && error.error)
                        errroMessage = error.error.message;
                    else errroMessage = error.message;

                    this._alertService.show("Ocorreu um erro", errroMessage, 'error')
                    return throwError(error);
                }
            }));
    }
}
