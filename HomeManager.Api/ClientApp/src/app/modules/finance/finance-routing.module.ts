import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/app/core/api-authorization/authorize.guard';

import { FinanceComponent } from './finance.component';
import { PaymentsComponent } from './payments/payments.component';

const routes: Routes = [
    { 
        path: 'finance', 
        component: FinanceComponent, 
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', redirectTo: '/finance', pathMatch: 'full' },
          { path: 'payments', component: PaymentsComponent }
        ]
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class FinanceRoutingModule { }
