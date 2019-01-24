import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest ,HttpHeaders} from '@angular/common/http';

@Injectable()
export class ParamInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (!req.url.includes('api/Account/')) {
            var token = JSON.parse(localStorage.getItem('token'));
            const paramReq = req.clone({
                headers: req.headers.set('Authorization','bearer ' + token['access_token'])
            });
            return next.handle(paramReq);
        } 
            return next.handle(req);
        

    }
}