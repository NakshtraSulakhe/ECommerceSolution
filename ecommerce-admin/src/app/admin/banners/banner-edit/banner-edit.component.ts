import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BannerService } from '../../../services/banner.service';

@Component({
  selector: 'app-banner-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './banner-edit.component.html',
  styleUrls: ['./banner-edit.component.scss']
})
export class BannerEditComponent implements OnInit {

  id!: number;
  title = '';
  bannerType = '';
  displayOrder = 1;
  isActive = true;
  redirectUrl = '';
  currentImageUrl = '';
  newImageFile?: File;

  constructor(
    private route: ActivatedRoute,
    private bannerService: BannerService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    this.bannerService.getById(this.id).subscribe((banner: any) => {
      this.title = banner.title;
      this.bannerType = banner.bannerType;
      this.displayOrder = banner.displayOrder;
      this.isActive = banner.isActive;
      this.redirectUrl = banner.redirectUrl;
      this.currentImageUrl = banner.imageUrl;
    });
  }

  onFileChange(event: any) {
    this.newImageFile = event.target.files[0];
  }

  update() {
    const form = new FormData();
    form.append("Title", this.title);
    form.append("BannerType", this.bannerType);
    form.append("DisplayOrder", this.displayOrder.toString());
    form.append("IsActive", String(this.isActive));
    form.append("RedirectUrl", this.redirectUrl);

    if (this.newImageFile) {
      form.append("ImageFile", this.newImageFile);
    }

    this.bannerService.update(this.id, form).subscribe(() => {
      this.router.navigate(['/admin/banners']);
    });
  }
}
