import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

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

  constructor(public userService: UserService, public routerServices: Router) { }

  onLogin() {
    Swal.fire({
      title: 'Iniciando sesión...',
      didOpen: () => {
        Swal.showLoading();
      },
      allowOutsideClick: false
    });

    this.userService.login(this.email, this.password).subscribe(
      (user: User) => {
        Swal.close();
        localStorage.setItem('sessionToken', JSON.stringify(user.token));
        localStorage.setItem('connectedUser', JSON.stringify(user));
        this.routerServices.navigate(['/manager/home']);
      },
      (error: HttpErrorResponse) => {
        Swal.close();
        if (error.status === 500) {
          Swal.fire('Error', 'Error al iniciar sesión, comuníquese con el administrador', 'error');
        } else {
          Swal.fire('Error', 'Correo o contraseña incorrecta', 'error');
        }
      }
    );
  }
}
