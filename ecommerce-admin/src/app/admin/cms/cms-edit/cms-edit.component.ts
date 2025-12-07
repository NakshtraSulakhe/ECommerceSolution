import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';
import { ActivatedRoute, Router } from '@angular/router';
import { CmsService } from '../../../services/cms.service';

@Component({
  selector: 'app-cms-edit',
  standalone: true,
  imports: [CommonModule, FormsModule, QuillModule],
  templateUrl: './cms-edit.component.html',
  styleUrls: ['./cms-edit.component.scss']
})
export class CmsEditComponent implements OnInit {
  id!: number; title=''; slug=''; content=''; isActive=true;
  constructor(private route: ActivatedRoute, private cms: CmsService, private router: Router){}
  ngOnInit(){
    this.id = Number(this.route.snapshot.params['id']);
    this.cms.getAllPages().subscribe((pages:any[])=>{
      const page = pages.find(p=>p.id===this.id);
      if(page){ this.title=page.title; this.slug=page.slug; this.content=page.content; this.isActive=page.isActive; }
    });
  }
  update(){
    this.cms.updatePage(this.id, {title:this.title, slug:this.slug, content:this.content, isActive:this.isActive})
      .subscribe(()=> this.router.navigate(['/admin/cms']));
  }
}
