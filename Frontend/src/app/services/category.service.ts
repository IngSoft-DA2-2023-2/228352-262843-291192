import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}
