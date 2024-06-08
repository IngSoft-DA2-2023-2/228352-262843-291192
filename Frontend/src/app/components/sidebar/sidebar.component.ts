import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { User } from '../../models/User';
import { UserRole } from '../../models/UserRole';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  user: User = {} as User;
  currentRole: UserRole = {} as UserRole;

  constructor(private router: Router, private route: ActivatedRoute) {
    /*
    this.router.events.subscribe((val) => {
      if (this.router.url) {
        this.currentRoute = this.router.url;
      }
    });*/
  }

  ngOnInit(): void {
    const sessionData = localStorage.getItem('connectedUser');
    if (sessionData) {
      this.user = JSON.parse(sessionData);
      this.currentRole = this.user.role;
    }
  }

  logout() {
    localStorage.removeItem('sessionToken');
    this.router.navigate(['/login']);
  }
}
