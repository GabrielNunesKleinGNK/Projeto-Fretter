import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { LoaderService } from "../services/loader.service";

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
    constructor(public loader: LoaderService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //this.loading.Exibir();
        if(!req.urlWithParams.includes("noload"))
            this.loader.show();
            
        return next.handle(req).pipe(
            finalize(() => {
                this.loader.hide();
            })
        );
    }
}