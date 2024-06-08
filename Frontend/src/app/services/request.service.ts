import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ManagerRequest } from '../models/ManagerRequest';
import { ListRequests } from '../models/ListRequests';

@Injectable({
    providedIn: 'root'
})
export class RequestService {

    constructor(private http: HttpClient) { }

    public getManagerRequests(): Observable<ListRequests> | null {
        return this.http.get<ListRequests>(`${environment.apiUrl}/requests/manager`);
    }

    public assignRequest(id: string, maintainerId: string): Observable<ManagerRequest> | null {
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this.http.put<ManagerRequest>(`${environment.apiUrl}/requests/${id}`, JSON.stringify(maintainerId), { headers });
    }
}