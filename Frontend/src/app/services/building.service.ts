import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Building } from '../models/Building';
import { BuildingDetails } from '../models/BuildingDetails';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {
  constructor(private http: HttpClient) { }

  public getBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(`${environment.apiUrl}/buildings`);
  }

  public getBuildingDetails(name: string): Observable<BuildingDetails> {
    return this.http.get<BuildingDetails>(`${environment.apiUrl}/buildings/${name}`);
  }
}