import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ManagerRequest } from '../models/ManagerRequest';

@Injectable({
    providedIn: 'root'
})
export class RequestService {

    constructor(private http: HttpClient) { }

    public getManagerRequests(): Observable<ManagerRequest[]> | null {
        let sessionToken = localStorage.getItem('sessionToken');
        if (sessionToken != null) {
            let token = JSON.parse(sessionToken);
            let headers = new HttpHeaders().set('Authorization', token);

            return this.http.get<ManagerRequest[]>(`${environment.apiUrl}/requests/manager`, { headers: headers});
        } else return null;
    }
}