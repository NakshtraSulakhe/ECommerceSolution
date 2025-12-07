import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BannerService } from '../../services/banner.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, NgFor],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  banners: any[] = [];

  constructor(private bannerService: BannerService) {}

  ngOnInit(): void {
    this.bannerService.getHomeBanners().subscribe((data: any) => {
      this.banners = data;
    });
  }
}
