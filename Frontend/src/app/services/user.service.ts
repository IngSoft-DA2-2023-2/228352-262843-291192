import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/User';
import { Maintainer, Maintainers } from '../models/Maintainers';
import { CreateMaintainer } from '../models/CreateMaintainer';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  public login(email: string, password: string): Observable<User> {
    const loginData = { email, password };
    return this.http.post<User>(`${environment.apiUrl}/login`, loginData);
  }

  public getMaintenanceStaff(): Observable<Maintainers> | null {
    return this.http.get<Maintainers>(`${environment.apiUrl}/maintenances`);
  }
  
  public createMaintainer(newMaintainer: CreateMaintainer): Observable<Maintainer> | null {
    return this.http.post<Maintainer>(`${environment.apiUrl}/maintenances`, newMaintainer);
  }
}
