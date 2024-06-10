import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Owner } from '../models/Owner';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OwnerService {

  constructor(private http: HttpClient) { }

  addOwner(owner: Owner): Observable<Owner> {
    return this.http.post<Owner>(`${environment.apiUrl}/owners`, owner);
  }
}
