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
import { adminRoleGuard } from './guards/admin-role.guard';
import { InviteComponent } from './components/invite/invite.component';
import { InvitationResponseComponent } from './components/invitation-response/invitation-response.component';
import { RequestsComponent } from './components/requests/requests.component';
import { MaintenanceComponent } from './components/maintenance/maintenance.component';
import { maintenanceRoleGuard } from './guards/maintenance-role.guard';
import { ConstructionCompanyComponent } from './components/construction-company/construction-company.component';
import { MaintainersComponent } from './components/maintainers/maintainers.component';
import { AdministrationComponent } from './components/administration/administration.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [authRedirectGuard], pathMatch: 'full'},
  { path: 'invitation-response', component: InvitationResponseComponent, pathMatch: 'full'},
  {
    path: 'manager', component: ManagerComponent, canActivate: [authGuard], children: [
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'buildings', component: BuildingsComponent, canActivate: [constructionCompanyAdminRoleGuard], pathMatch: 'full' },
      { path: 'buildings/create', component: CreateBuildingComponent, canActivate: [constructionCompanyAdminRoleGuard], pathMatch: 'full' },
      { path: 'buildings/:id', component: BuildingDetailComponent, canActivate: [constructionCompanyAdminRoleGuard] },
      { path: 'reports', component: ReportsComponent, canActivate: [managerRoleGuard], pathMatch: 'full' },
      { path: 'requests', component: RequestsComponent, canActivate: [managerRoleGuard], pathMatch: 'full' },
      { path: 'maintenance', component: MaintenanceComponent, canActivate: [maintenanceRoleGuard], pathMatch: 'full' },
      { path: 'constructioncomany', component: ConstructionCompanyComponent, canActivate: [constructionCompanyAdminRoleGuard], pathMatch: 'full' },
      { path: 'maintainers', component: MaintainersComponent, canActivate: [managerRoleGuard], pathMatch: 'full' },
      { path: 'invite', component: InviteComponent, canActivate: [adminRoleGuard], pathMatch: 'full'},
      { path: 'administration', component: AdministrationComponent, pathMatch: 'full'}
    ],
  },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }