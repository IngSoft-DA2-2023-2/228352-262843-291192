import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Manager } from '../models/Manager';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  constructor(private http: HttpClient) { }

  getManagers(): Observable<Manager[]> {
    return this.http.get<Manager[]>(`${environment.apiUrl}/managers`);
  }

  createAdmin(adminData: User): Observable<any> {
    return this.http.post<User>(`${environment.apiUrl}/admins`, adminData);
  }

  deleteManager(id: string): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/managers/${id}`);
  }
}
