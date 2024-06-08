import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { authGuard } from './guards/auth.guard';
import { authRedirectGuard } from './guards/auth-redirect.guard';
import { NgModule } from '@angular/core';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { ManagerComponent } from './components/manager/manager.component';
import { BuildingDetailComponent } from './components/building-detail/building-detail.component';
import { ReportsComponent } from './components/reports/reports.component';
import { CreateBuildingComponent } from './components/create-building/create-building.component';
import { constructionCompanyAdminRoleGuard } from './guards/construction-company-admin-role.guard';
import { managerRoleGuard } from './guards/manager-role.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [authRedirectGuard], pathMatch: 'full'},
  { path: 'manager', component: ManagerComponent, canActivate: [authGuard], children: [
    { path: 'home', component: HomeComponent, pathMatch: 'full'},
    { path: 'buildings', component: BuildingsComponent, canActivate: [constructionCompanyAdminRoleGuard], pathMatch: 'full'},
    { path: 'buildings/create', component: CreateBuildingComponent, canActivate: [constructionCompanyAdminRoleGuard], pathMatch: 'full'},
    { path: 'buildings/:id', component: BuildingDetailComponent, canActivate: [constructionCompanyAdminRoleGuard]},
    { path: 'reports', component: ReportsComponent, canActivate: [managerRoleGuard], pathMatch: 'full'},
  ]},
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }