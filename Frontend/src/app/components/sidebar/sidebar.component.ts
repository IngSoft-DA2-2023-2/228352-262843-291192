import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
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
    this.router.navigate(['/login']);
  }
}
