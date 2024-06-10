import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { ManagerService } from '../../services/manager.service';
import { CommonModule } from '@angular/common';
import { Manager } from '../../models/Manager';

@Component({
  selector: 'app-administration',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './administration.component.html',
  styleUrl: './administration.component.css'
})
export class AdministrationComponent {
  adminForm: FormGroup;
  managers: Manager[] = [];

  private errorMessages: { [key: string]: string } = {
    "Email already exists": "El correo ya esta en uso.",
  };

  constructor(private fb: FormBuilder, private managerService: ManagerService) {
    this.adminForm = this.fb.group({
      name: ['', [Validators.required]],
      lastname: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadManagers();
  }

  loadManagers(): void {
    this.managerService.getManagers().subscribe(
      (response: any) => {
        if (response && Array.isArray(response.managers)) {
          this.managers = response.managers;
          console.log('Managers:', this.managers);
        } else {
          console.error('Los datos recibidos no son válidos:', response);
        }
      },
      (error: any) => {
        console.log('Error al obtener edificios:', error);
      }
    );
  }

  createAdmin(): void {
    if (this.adminForm.invalid) {
      return;
    }

    this.managerService.createAdmin(this.adminForm.value).subscribe(
      response => {
        Swal.fire('Éxito', 'Administrador creado con éxito.', 'success');
        this.adminForm.reset();
      },
      error => {
        const errorMessage = this.errorMessages[error.error.errorMessage];

        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: errorMessage|| 'Ocurrió un error al crear la invitación.',
          confirmButtonText: 'Aceptar'
        });
      }
    );
  }

  deleteManager(id: string): void {
    Swal.fire({
      title: '¿Estás seguro?',
      text: "¡No podrás revertir esto!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminar!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.managerService.deleteManager(id).subscribe(
          response => {
            Swal.fire('Eliminado', 'El encargado ha sido eliminado.', 'success');
            this.loadManagers();
          },
          error => {
            Swal.fire('Error', 'No se pudo eliminar el encargado.', 'error');
          }
        );
      }
    });
  }
}
