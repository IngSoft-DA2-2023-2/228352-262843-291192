import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { ManagerComponent } from './components/manager/manager.component';
import { ReportsComponent } from './components/reports/reports.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, LoginComponent, ManagerComponent, HomeComponent, SidebarComponent, ReportsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BuildingManager';
}
