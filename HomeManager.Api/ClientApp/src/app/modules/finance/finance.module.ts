import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinanceRoutingModule } from './finance-routing.module';

import { FinanceComponent } from './finance.component';
import { PaymentsComponent } from './payments/payments.component';

@NgModule({
  declarations: [
    FinanceComponent,
    PaymentsComponent
  ],
  exports: [
    FinanceComponent,
    PaymentsComponent
  ],
  imports: [
    CommonModule,
    FinanceRoutingModule
  ]
})
export class FinanceModule { }
