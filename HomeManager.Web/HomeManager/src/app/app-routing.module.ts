import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminComponent } from './admin/admin.component';
import { CustomizeComponent } from './customize/customize.component';
import { SettingsComponent } from './settings/settings.component';
import { FinanceComponent } from './finance/finance.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent},
  { path: 'admin', component: AdminComponent},
  { path: 'customize', component: CustomizeComponent},
  { path: 'settings', component: SettingsComponent},
  { path: 'finance', component: FinanceComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
