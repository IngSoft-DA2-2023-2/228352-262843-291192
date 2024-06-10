import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Building } from '../models/Building';
import { BuildingDetails } from '../models/BuildingDetails';
import { ManagerBuildings } from '../models/ManagerBuilding';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {
  constructor(private http: HttpClient) { }

  public getBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(`${environment.apiUrl}/buildings`);
  }

  public getBuildingDetails(id: string): Observable<BuildingDetails> {
    return this.http.get<BuildingDetails>(`${environment.apiUrl}/buildings/${id}`);
  }

  public updateBuilding(id: string, building: BuildingDetails): Observable<BuildingDetails> {
    return this.http.put<BuildingDetails>(`${environment.apiUrl}/buildings/${id}`, building);
  }

  public createBuilding(building: BuildingDetails): Observable<BuildingDetails> {
    return this.http.post<BuildingDetails>(`${environment.apiUrl}/buildings`, building);
  }

  public deleteBuilding(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/buildings/${id}`);
  }

  public getManagerBuildings(): Observable<ManagerBuildings> | null {
    let managerConnected = localStorage.getItem('connectedUser');
    if (managerConnected == null) return null;
    let managerConnectedJson = JSON.parse(managerConnected);
    let sessionToken = localStorage.getItem('sessionToken');
    if (sessionToken != null) {
      let token = JSON.parse(sessionToken);
      let headers = new HttpHeaders().set('Authorization', token);
      return this.http.get<ManagerBuildings>(`${environment.apiUrl}/managers/${managerConnectedJson.userId}/buildings`, { headers: headers });
    } else return null
  }

  public updateBuildingManager(buildingId: string, managerId: string): Observable<any> {
    const url = `${environment.apiUrl}/buildings/${buildingId}/managers`;
    return this.http.put(url, { managerId: managerId });
  }
}