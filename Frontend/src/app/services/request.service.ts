import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ManagerRequest } from '../models/ManagerRequest';
import { ListRequests } from '../models/ListRequests';

@Injectable({
    providedIn: 'root'
})
export class RequestService {

    constructor(private http: HttpClient) { }

    public getManagerRequests(filter: string): Observable<ListRequests> | null {
        let params = new HttpParams().set('category', filter);
        return this.http.get<ListRequests>(`${environment.apiUrl}/requests/manager`, { params: params });
    }

    public assignRequest(id: string, maintainerId: string): Observable<ManagerRequest> | null {
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this.http.put<ManagerRequest>(`${environment.apiUrl}/requests/${id}`, JSON.stringify(maintainerId), { headers });
    }
}