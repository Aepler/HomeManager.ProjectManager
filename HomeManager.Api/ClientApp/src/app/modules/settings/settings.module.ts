import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsRoutingModule } from './settings-routing.module';

import { SettingsComponent } from './settings.component';
import { AccountComponent } from './account/account.component';
import { FinanceComponent } from './finance/finance.component';

@NgModule({
  declarations: [
    SettingsComponent,
    AccountComponent,
    FinanceComponent
  ],
  exports: [
    SettingsComponent,
    AccountComponent,
    FinanceComponent
  ],
  imports: [
    CommonModule,
    SettingsRoutingModule
  ]
})
export class SettingsModule { }
