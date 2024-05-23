import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [UserService]
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(public userService: UserService) { }

  onLogin() {
    this.userService.login(this.email, this.password).subscribe(
      (user: User) => {
        this.userService.login(this.email, this.password);
        console.log('User logged in:', user);
      },
      (error) => console.error('Error logging in:', error)
    );
  }
}
