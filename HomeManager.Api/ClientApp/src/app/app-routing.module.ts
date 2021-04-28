import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from './core/guards/authorize.guard';


const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { 
    path: 'home', 
    loadChildren: () =>
            import('./modules/home/home-routing.module').then(m => m.HomeRoutingModule)
  },
  { 
    path: 'admin', 
    canActivate: [AuthorizeGuard],
    loadChildren: () =>
            import('./modules/admin/admin-routing.module').then(m => m.AdminRoutingModule)
  },
  { 
    path: 'customize', 
    canActivate: [AuthorizeGuard],
    loadChildren: () =>
            import('./modules/customize/customize-routing.module').then(m => m.CustomizeRoutingModule)
  },
  { 
    path: 'dashboard', 
    canActivate: [AuthorizeGuard],
    loadChildren: () =>
            import('./modules/dashboard/dashboard-routing.module').then(m => m.DashboardRoutingModule)
  },
  { 
    path: 'settings', 
    canActivate: [AuthorizeGuard],
    loadChildren: () =>
            import('./modules/settings/settings-routing.module').then(m => m.SettingsRoutingModule)
  },
  { 
    path: 'finance', 
    canActivate: [AuthorizeGuard],
    loadChildren: () =>
            import('./modules/finance/finance-routing.module').then(m => m.FinanceRoutingModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
