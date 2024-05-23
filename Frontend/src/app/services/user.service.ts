import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  public login(email: string, password: string): Observable<User> {
    const loginData = { email, password };
    return this.http.post<User>(`${environment.apiUrl}/login`, loginData);
  }
}
