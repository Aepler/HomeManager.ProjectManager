import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';

import { AdminComponent } from './admin.component';
import { ManageComponent } from './manage/manage.component';
import { FinanceComponent } from './finance/finance.component';



@NgModule({
  declarations: [
    AdminComponent,
    ManageComponent,
    FinanceComponent
  ],
  exports: [
    AdminComponent,
    ManageComponent,
    FinanceComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
