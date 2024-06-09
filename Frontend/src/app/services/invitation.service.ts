import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Invitation, InvitationList } from '../models/Invitation';

@Injectable({
  providedIn: 'root'
})

export class InvitationService {
  constructor(private http: HttpClient) { }

  public createInvitation(invitation: Invitation): Observable<Invitation> {
    return this.http.post<Invitation>(`${environment.apiUrl}/invitations`, invitation);
  }

  public getInvitationByEmail(email: string): Observable<InvitationList> {
    const url = `${environment.apiUrl}/invitations?email=${email}`;
    return this.http.get(url) as Observable<InvitationList>;
  }

  public respondToInvitation(id: string, email: string, password: string, status: number): Observable<any> {
    const url = `${environment.apiUrl}/invitations/${id}/response`;
    const body = {
      status,
      email,
      password
    };
    return this.http.post(url, body);
  }

}
  