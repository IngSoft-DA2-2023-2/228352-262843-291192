import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Building } from '../models/Building';
import { ManagerBuilding, ManagerBuildings } from '../models/ManagerBuilding';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http: HttpClient) { }
  
  public getBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(`${environment.apiUrl}/buildings`);
  }

  public getManagerBuildings(managerId: string): Observable<ManagerBuildings> | null {
    let sessionToken = localStorage.getItem('sessionToken');
    if (sessionToken != null) {
      let token = JSON.parse(sessionToken);
      let headers = new HttpHeaders().set('Authorization', token.token);
      return this.http.get<ManagerBuildings>(`${environment.apiUrl}/managers/${managerId}/buildings`, { headers: headers });
    } else return null
  }
}