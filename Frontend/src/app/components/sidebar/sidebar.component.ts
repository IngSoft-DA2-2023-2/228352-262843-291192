import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { UserRole } from '../../enums/UserRole';
import { User } from '../../models/User';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, NgIf],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  currentRoute: string | undefined;

  constructor(private router: Router, private route: ActivatedRoute) {
    this.router.events.subscribe((val) => {
      if (this.router.url) {
        this.currentRoute = this.router.url;
      }
    });
  }

  logout() {
    localStorage.removeItem('sessionToken');
    localStorage.removeItem('connectedUser');
    this.router.navigate(['/login']);
  }

  isAdmin() {
    const connectedUser = JSON.parse(localStorage.getItem('connectedUser') as string) as User;
    return connectedUser.role === UserRole.ADMIN;
  }

  isManager() {
    const connectedUser = JSON.parse(localStorage.getItem('connectedUser') as string) as User;
    return connectedUser.role === UserRole.MANAGER;
  }

  isMaintenance() {
    const connectedUser = JSON.parse(localStorage.getItem('connectedUser') as string) as User;
    return connectedUser.role === UserRole.MAINTENANCE;
  }

  isCompanyAdmin() {
    const connectedUser = JSON.parse(localStorage.getItem('connectedUser') as string) as User;
    return connectedUser.role === UserRole.CONSTRUCTIONCOMPANYADMIN;
  }
}
