import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class BannerService {
  private baseUrl = 'https://localhost:7000/api/banners';

    constructor(private http: HttpClient) { }

     getHomeBanners(): Observable<any> {
    return this.http.get(`${this.baseUrl}?type=home`);
  }

  getCategoryBanner(categoryId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}?type=category&categoryId=${categoryId}`);
  }
}