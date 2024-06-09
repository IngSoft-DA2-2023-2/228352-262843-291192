import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../models/Category';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {

    constructor(private http: HttpClient) { }

    public getCategories(): Observable<Category[]> | null {
        return this.http.get<{ categories: Category[] }>(`${environment.apiUrl}/categories`)
            .pipe(map(response => response.categories));
    }

    public assignParent(childId: string, parentId: string): Observable<Category> | null {
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this.http.put<Category>(`${environment.apiUrl}/categories/${childId}`, JSON.stringify(parentId), { headers });
    }
}
