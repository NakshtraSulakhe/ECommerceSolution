import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CmsService } from '../../../services/cms.service';

@Component({
  selector: 'app-cms-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './cms-list.component.html',
  styleUrl: './cms-list.component.scss'
})
export class CmsListComponent implements OnInit {
 pages: any[] = [];
 constructor(private cms: CmsService, private router: Router) {}

 ngOnInit(): void {
    this.cms.getAllPages().subscribe((res: any) => this.pages = res);
 }
  editPage(id: number){ this.router.navigate(['/admin/cms/edit', id]); }
  deletePage(id: number){ if(confirm('Delete?')) this.cms.deletePage(id).subscribe(()=> this.pages = this.pages.filter(p=>p.id!==id)); }


}
