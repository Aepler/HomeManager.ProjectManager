import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AdminComponent } from './admin.component';
import { ManageComponent } from './manage/manage.component';
import { FinanceComponent } from './finance/finance.component';



@NgModule({
  declarations: [
    AdminComponent,
    ManageComponent,
    FinanceComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ]
})
export class AdminModule { }
