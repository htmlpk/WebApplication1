import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class ParamInterceptor implements HttpInterceptor {
    constructor(private router: Router) {
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (!req.url.includes('api/Account/')) {
            var token = JSON.parse(localStorage.getItem('token'));
            const paramReq = req.clone({
                headers: req.headers.set('Authorization', 'bearer ' + token['access_token']),
            });
            return next.handle(paramReq).pipe(
                catchError((error: HttpErrorResponse) => {
                    console.error(error);
                    localStorage.setItem('error', JSON.stringify(error['error']));
                    this.router.navigate(['/error']);
                    return throwError("errMsg");
                }));
        }
        return next.handle(req);
    }
}