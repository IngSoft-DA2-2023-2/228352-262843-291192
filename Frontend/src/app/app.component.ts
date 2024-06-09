import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { ManagerComponent } from './components/manager/manager.component';
import { HttpClientModule } from '@angular/common/http';
import { ManagerReportsComponent } from './components/manager-reports/manager-reports.component';
import { RequestsComponent } from './components/requests/requests.component';
import { MaintainersComponent } from './components/maintainers/maintainers.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { AdminReportsComponent } from './components/admin-reports/admin-reports.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, LoginComponent, ManagerComponent, HomeComponent,
    SidebarComponent, HttpClientModule, ManagerReportsComponent, RequestsComponent, MaintainersComponent,
    CategoriesComponent, AdminReportsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BuildingManager';
}
