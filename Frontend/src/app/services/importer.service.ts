import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImporterService {

  constructor(private http: HttpClient) { }

  public getImporters(): Observable<string[]> {
    return this.http.get<string[]>(`${environment.apiUrl}/importers`);
  }

  public importData(importerName: string, buildingsData: string): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(`${environment.apiUrl}/importers/${importerName}`, JSON.stringify(buildingsData), {headers: headers});
  }
}