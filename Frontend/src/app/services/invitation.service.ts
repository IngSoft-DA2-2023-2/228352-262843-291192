import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Invitation } from '../models/Invitation';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {
  constructor(private http: HttpClient) { }

  public createInvitation(invitation: Invitation): Observable<Invitation> {
    invitation.deadline += 10800;
    return this.http.post<Invitation>(`${environment.apiUrl}/invitations`, invitation);
  }

  getInvitations(expiredOrNear: boolean, status: number = 2): Observable<Invitation[]> {
    let params = new HttpParams()
      .set('expiredOrNear', expiredOrNear.toString())
      .set('status', status.toString());
    return this.http.get(`${environment.apiUrl}/invitations`, { params }) as Observable<Invitation[]>;
  }

  deleteInvitation(invitationId: string): Observable<Invitation> {
    return this.http.delete(`${environment.apiUrl}/invitations/${invitationId}`) as Observable<Invitation>;
  }

  updateInvitationDeadline(invitationId: string, editedDeadline: number): Observable<Invitation> {
    let addedDeadline3Hours = editedDeadline + 10800;
    return this.http.put(`${environment.apiUrl}/invitations/${invitationId}`, { newDeadline: addedDeadline3Hours }) as Observable<Invitation>;
  }

  public getInvitationByEmail(email: string): Observable<Invitation[]> {
    const url = `${environment.apiUrl}/invitations?email=${email}`;
    return this.http.get(url) as Observable<Invitation[]>;
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
