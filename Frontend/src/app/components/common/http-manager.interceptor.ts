import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HttpManagerInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const sessionData = localStorage.getItem('connectedUser');
        const token = sessionData ? JSON.parse(sessionData).token : null;
        if (token) {
            const newRequest = request.clone({
                setHeaders: {
                    'Authorization': `${token}`
                }
            });
            return next.handle(newRequest);
        } else {
            return next.handle(request);
        }
    }
}