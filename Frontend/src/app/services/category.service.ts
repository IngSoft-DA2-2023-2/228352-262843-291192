import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../models/Category';
import { CreateCategory } from '../models/CreateCategory';

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

    public createCategory(newCategory: CreateCategory): Observable<Category> | null {
        return this.http.post<Category>(`${environment.apiUrl}/categories`, newCategory);
    }
}
