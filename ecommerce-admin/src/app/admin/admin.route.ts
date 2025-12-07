import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { CmsListComponent } from './cms/cms-list/cms-list.component';
import { CmsAddComponent } from './cms/cms-add/cms-add.component';
import { CmsEditComponent } from './cms/cms-edit/cms-edit.component';


export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      { path: 'cms', component: CmsListComponent },
      { path: 'cms/add', component: CmsAddComponent },
      { path: 'cms/edit/:id', component: CmsEditComponent },
      // placeholders for future
      { path: 'banners', loadChildren: () => import('./banners/banners.route').then(m => m.BANNER_ROUTES) },
    //   { path: '', redirectTo: 'cms', pathMatch: 'full' }
     ]
  }
];