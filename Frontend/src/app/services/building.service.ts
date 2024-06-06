import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Building } from '../models/Building';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {
  constructor(private http: HttpClient) { }

  public getBuildings(): Observable<Building[]> {
    const sessionData = localStorage.getItem('sessionToken');
    const token = sessionData ? JSON.parse(sessionData).token : null;
    const headers = new HttpHeaders({
      'Authorization': `${token}`
    });
    
    return this.http.get<Building[]>(`${environment.apiUrl}/buildings`, { headers });
  }
}