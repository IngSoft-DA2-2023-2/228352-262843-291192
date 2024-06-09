import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NgIf } from '@angular/common';
import { User } from '../../models/User';
import { CommonModule } from '@angular/common';
import { UserRole } from '../../enums/UserRole';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, NgIf],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  user: User = {} as User;
  currentRole: UserRole = {} as UserRole;

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const sessionData = localStorage.getItem('connectedUser');
    if (sessionData) {
      this.user = JSON.parse(sessionData);
      this.currentRole = this.user.role;
    }
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
