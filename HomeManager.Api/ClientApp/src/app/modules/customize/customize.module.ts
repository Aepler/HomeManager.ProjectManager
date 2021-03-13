import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomizeRoutingModule } from './customize-routing.module';

import { CustomizeComponent } from './customize.component';
import { FinanceComponent } from './finance/finance.component';



@NgModule({
  declarations: [
    CustomizeComponent,
    FinanceComponent
  ],
  exports: [
    CustomizeComponent,
    FinanceComponent
  ],
  imports: [
    CommonModule,
    CustomizeRoutingModule
  ]
})
export class CustomizeModule { }
