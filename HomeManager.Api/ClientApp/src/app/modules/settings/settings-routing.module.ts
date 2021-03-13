import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/app/core/api-authorization/authorize.guard';

import { SettingsComponent } from './settings.component';
import { AccountComponent } from './account/account.component';
import { FinanceComponent } from './finance/finance.component';

const routes: Routes = [
    { 
        path: 'settings', 
        component: SettingsComponent, 
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', redirectTo: '/settings', pathMatch: 'full' },
          { path: 'account', component: AccountComponent },
          { path: 'finance', component: FinanceComponent }
        ]
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
