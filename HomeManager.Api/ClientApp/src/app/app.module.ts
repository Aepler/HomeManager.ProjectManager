import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { DataTablesModule } from "angular-datatables";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';
import { AuthorizeGuard } from './core/guards/authorize.guard';
import { AuthorizeInterceptor } from './core/interceptors/authorize.interceptor';

import { AdminModule } from './modules/admin/admin.module';
import { CustomizeModule } from './modules/customize/customize.module';
import { FinanceModule } from './modules/finance/finance.module';
import { SettingsModule } from './modules/settings/settings.module';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';
import { DashboardModule } from './modules/dashboard/dashboard.module';
import { HomeModule } from './modules/home/home.module';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    DataTablesModule,
    FormsModule,
    CoreModule,
    AdminModule,
    CustomizeModule,
    FinanceModule,
    SettingsModule,
    DashboardModule,
    HomeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
