import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseUrl = "https://localhost:7000/api/products";

  constructor(private http: HttpClient) {}

  // Home: Latest or Featured products
  getFeatured(): Observable<any> {
    return this.http.get(`${this.baseUrl}/featured`);
  }

  // Category Page Products
  getByCategory(categoryId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/by-category/${categoryId}`);
  }

  // Later: product detail
  getById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }
}
