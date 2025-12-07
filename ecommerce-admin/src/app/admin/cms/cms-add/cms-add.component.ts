import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';
import { Router } from '@angular/router';
import { CmsService } from '../../../services/cms.service';

@Component({
  selector: 'app-cms-add',
  standalone: true,
  imports: [CommonModule, FormsModule, QuillModule],
  templateUrl: './cms-add.component.html',
  styleUrls: ['./cms-add.component.scss']
})
export class CmsAddComponent {
  title = ''; slug = ''; content = '';
  constructor(private cms: CmsService, private router: Router) {}
  save(){
    this.cms.createPage({title:this.title, slug:this.slug, content:this.content})
      .subscribe(()=> this.router.navigate(['/admin/cms']));
  }
}
