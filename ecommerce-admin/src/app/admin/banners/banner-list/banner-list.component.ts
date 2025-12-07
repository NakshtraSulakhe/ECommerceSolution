import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BannerService } from '../../../services/banner.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-banner-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './banner-list.component.html',
  styleUrls: ['./banner-list.component.scss']
})
export class BannerListComponent implements OnInit {

  banners: any[] = [];

  constructor(private bannerService: BannerService, private router: Router) {}

  ngOnInit(): void {
    this.bannerService.getAll().subscribe((res: any) => {
      this.banners = res;
    });
  }

  edit(id: number) {
    this.router.navigate(['/admin/banners/edit', id]);
  }

  delete(id: number) {
    if (confirm("Delete banner?")) {
      this.bannerService.delete(id).subscribe(() => {
        this.banners = this.banners.filter(b => b.id !== id);
      });
    }
  }
}
