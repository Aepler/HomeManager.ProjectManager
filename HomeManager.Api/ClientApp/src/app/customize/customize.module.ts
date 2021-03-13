import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { CustomizeComponent } from './customize.component';



@NgModule({
  declarations: [
    CustomizeComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ]
})
export class CustomizeModule { }
