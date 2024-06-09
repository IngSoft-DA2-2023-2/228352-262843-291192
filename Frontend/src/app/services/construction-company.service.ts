import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConstructionCompany } from '../models/ConstructionCompany';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/User';
import { ConstructionCompanyAdmin } from '../models/ConstructionCompanyAdmin';
import { Building } from '../models/Building';

@Injectable({
  providedIn: 'root'
})
export class ConstructionCompanyService {

  constructor(private http: HttpClient) { }

  createConstructionCompany(company: ConstructionCompany): Observable<ConstructionCompany> {
    return this.http.post<ConstructionCompany>(`${environment.apiUrl}/construction-company`, company);
  }

  modifyConstructionCompanyName(company: ConstructionCompany): Observable<ConstructionCompany> {
    return this.http.put<ConstructionCompany>(`${environment.apiUrl}/construction-company/${company.id}`, company);
  }

  getConstructionCompanyByUser(): Observable<ConstructionCompany> {
    return this.http.get<ConstructionCompany>(`${environment.apiUrl}/construction-company-admins`);
  }

  inviteConstructionCompanyAdmin(ccadmin: ConstructionCompanyAdmin): Observable<User> {
    return this.http.post<User>(`${environment.apiUrl}/construction-company-admins`, ccadmin);
  }

  getBuildingsFromCCAdmin(ccadminId: string): Observable<Building[]> {
    return this.http.get<Building[]>(`${environment.apiUrl}/construction-company-admins/${ccadminId}/buildings`);
  }
}
