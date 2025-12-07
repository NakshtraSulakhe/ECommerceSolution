import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BannerService } from '../../services/banner.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  categoryId!: number;
  banner: any;

  constructor(
    private route: ActivatedRoute,
    private bannerService: BannerService
  ) {}

  ngOnInit(): void {
    this.categoryId = Number(this.route.snapshot.paramMap.get('id'));

    this.bannerService.getCategoryBanner(this.categoryId)
      .subscribe((res: any) => {
        this.banner = res.length > 0 ? res[0] : null;
      });
  }
}
