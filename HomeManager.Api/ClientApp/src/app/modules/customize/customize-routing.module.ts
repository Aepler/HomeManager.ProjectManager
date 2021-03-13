import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/app/core/api-authorization/authorize.guard';

import { CustomizeComponent } from './customize.component';
import { FinanceComponent } from './finance/finance.component';

const routes: Routes = [
    { 
        path: 'customize', 
        component: CustomizeComponent, 
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', redirectTo: '/customize', pathMatch: 'full' },
          { path: 'finance', component: FinanceComponent }
        ]
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class CustomizeRoutingModule { }
