import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { authGuard } from './guards/auth.guard';
import { authRedirectGuard } from './guards/auth-redirect.guard';
import { NgModule } from '@angular/core';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { ManagerComponent } from './components/manager/manager.component';
import { ReportsComponent } from './components/reports/reports.component';
import { RequestsComponent } from './components/requests/requests.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [authRedirectGuard], pathMatch: 'full' },
  {
    path: 'manager', component: ManagerComponent, canActivate: [authGuard], children: [
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'buildings', component: BuildingsComponent, pathMatch: 'full' },
      { path: 'reports', component: ReportsComponent, pathMatch: 'full' },
      { path: 'requests', component: RequestsComponent, pathMatch: 'full' },
    ]
  },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }