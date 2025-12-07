import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CmsService {

  private baseUrl = 'https://localhost:7000/api/cms';

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) {}

  // Attach JWT token if available
  private getAuthHeaders() {
    const token = this.auth.getToken();
    return {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      })
    };
  }

  // GET all CMS pages
  getAllPages(): Observable<any> {
    return this.http.get(`${this.baseUrl}/all`, this.getAuthHeaders());
  }

  // GET page by id
  getPageById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`, this.getAuthHeaders());
  }

  // GET page by slug (for frontend later)
  getPageBySlug(slug: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/slug/${slug}`);
  }

  // CREATE a new CMS page
  createPage(data: any): Observable<any> {
    return this.http.post(this.baseUrl, data, this.getAuthHeaders());
  }

  // UPDATE CMS page
  updatePage(id: number, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, data, this.getAuthHeaders());
  }

  // DELETE CMS page
  deletePage(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`, this.getAuthHeaders());
  }
}
