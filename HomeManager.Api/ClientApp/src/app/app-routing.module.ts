import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminComponent } from './admin/admin.component';
import { ManageComponent } from './admin/manage/manage.component';
import { CustomizeComponent } from './customize/customize.component';
import { FinanceComponent } from './finance/finance.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'dashboard', component: DashboardComponent},
  { 
    path: 'admin', 
    component: AdminComponent,
    children: [
      {path: '', redirectTo: '/admin', pathMatch: 'full'},
      { path: 'manage', component: ManageComponent}
    ]
  },
  { path: 'customize', component: CustomizeComponent},
  { path: 'finance', component: FinanceComponent},
  { path: 'settings', component: SettingsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
