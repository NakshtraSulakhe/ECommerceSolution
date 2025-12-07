import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BannerService } from '../../../services/banner.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-banner-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './banner-add.component.html',
  styleUrls: ['./banner-add.component.scss']
})
export class BannerAddComponent {

  title = '';
  bannerType = 'home';
  displayOrder = 1;
  isActive = true;
  redirectUrl = '';
  file!: File;

  constructor(private bannerService: BannerService, private router: Router) {}

  onFileChange(event: any) {
    this.file = event.target.files[0];
  }

  save() {
    const form = new FormData();
    form.append("Title", this.title);
    form.append("BannerType", this.bannerType);
    form.append("DisplayOrder", this.displayOrder.toString());
    form.append("IsActive", String(this.isActive));
    form.append("RedirectUrl", this.redirectUrl);
    form.append("ImageFile", this.file);

    this.bannerService.create(form).subscribe(() => {
      this.router.navigate(['/admin/banners']);
    });
  }
}
