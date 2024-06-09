import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NgIf } from '@angular/common';
import { CommonModule } from '@angular/common';
import { UserRole } from '../../enums/UserRole';
import { User } from '../../models/User';

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
}
