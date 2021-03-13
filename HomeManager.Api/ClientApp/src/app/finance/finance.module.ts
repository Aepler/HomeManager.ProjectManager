import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { FinanceComponent } from './finance.component';

@NgModule({
  declarations: [
    FinanceComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ]
})
export class FinanceModule { }
