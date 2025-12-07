import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BannerService {

  private baseUrl = 'https://localhost:7000/api/banners';

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) {}

  private authHeaders() {
    return {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.auth.getToken()}`
      })
    };
  }

  getAll(): Observable<any> {
    return this.http.get(`${this.baseUrl}`, this.authHeaders());
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`, this.authHeaders());
  }

  create(formData: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}`, formData, this.authHeaders());
  }

  update(id: number, formData: FormData): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, formData, this.authHeaders());
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`, this.authHeaders());
  }
}
