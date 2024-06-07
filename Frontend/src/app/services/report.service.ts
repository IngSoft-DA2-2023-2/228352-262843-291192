import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { BuildingsReport } from '../models/BuildingsReport';
import { MaintenancesReport } from '../models/MaintenancesReport';
import { ApartmentsReport } from '../models/ApartmentsReport';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }
  
  public getBuildingsReport(filter: string): Observable<BuildingsReport> | null {
    let sessionToken = localStorage.getItem('sessionToken');
    if (sessionToken != null) {
      let token = JSON.parse(sessionToken);
      let headers = new HttpHeaders().set('Authorization', token);
      let params = new HttpParams().set('buildingId', filter);

      return this.http.get<BuildingsReport>(`${environment.apiUrl}/reports`, { headers: headers, params: params } );
    } else return null;
  }

  public getMaintenanceReport(buildingId: string, filter: string): Observable<MaintenancesReport> | null {
    let sessionToken = localStorage.getItem('sessionToken');
    if (sessionToken != null) {
      let token = JSON.parse(sessionToken);
      let headers = new HttpHeaders().set('Authorization', token);
      let params = new HttpParams().set('maintainerName', filter);

      return this.http.get<MaintenancesReport>(`${environment.apiUrl}/reports/${buildingId}/maintenances`, { headers: headers, params: params});
    } else return null;
  }

  public getApartmentsReport(buildingId: string): Observable<ApartmentsReport> | null {
    let sessionToken = localStorage.getItem('sessionToken');
    if (sessionToken != null) {
      let token = JSON.parse(sessionToken);
      let headers = new HttpHeaders().set('Authorization', token);

      return this.http.get<ApartmentsReport>(`${environment.apiUrl}/reports/${buildingId}/apartments`, { headers: headers });
    } else return null;
  }
}