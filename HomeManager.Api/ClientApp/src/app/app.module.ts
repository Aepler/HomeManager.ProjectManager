import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { DataTablesModule } from "angular-datatables";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ApiAuthorizationModule } from './core/api-authorization/api-authorization.module';
import { AuthorizeGuard } from './core/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from './core/api-authorization/authorize.interceptor';

import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { AdminModule } from './modules/admin/admin.module';
import { CustomizeModule } from './modules/customize/customize.module';
import { FinanceModule } from './modules/finance/finance.module';
import { SettingsModule } from './modules/settings/settings.module';
import { HomeComponent } from './modules/home/home.component';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    HomeComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    DataTablesModule,
    FormsModule,
    ApiAuthorizationModule,
    AdminModule,
    CustomizeModule,
    FinanceModule,
    SettingsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
