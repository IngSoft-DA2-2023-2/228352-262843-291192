import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConstructionCompany } from '../models/ConstructionCompany';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/User';
import { ConstructionCompanyAdmin } from '../models/ConstructionCompanyAdmin';

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
    return this.http.get<ConstructionCompany>(`${environment.apiUrl}/construction-company-admin`);
  }

  inviteConstructionCompanyAdmin(ccadmin: ConstructionCompanyAdmin): Observable<User> {
    console.log('Inviting construction company admin', ccadmin)
    return this.http.post<User>(`${environment.apiUrl}/construction-company-admin`, ccadmin);
  }
}
