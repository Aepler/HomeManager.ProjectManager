import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/app/core/api-authorization/authorize.guard';

import { AdminComponent } from './admin.component';
import { ManageComponent } from './manage/manage.component';
import { FinanceComponent } from './finance/finance.component';

const routes: Routes = [
    { 
        path: 'admin', 
        component: AdminComponent,
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', redirectTo: '/admin', pathMatch: 'full' },
          { path: 'manage', component: ManageComponent},
          { path: 'finance', component: FinanceComponent}
        ]
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
