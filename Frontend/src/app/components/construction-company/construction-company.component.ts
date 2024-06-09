import { Component } from '@angular/core';
import { User } from '../../models/User';
import { ConstructionCompany } from '../../models/ConstructionCompany';
import { ConstructionCompanyService } from '../../services/construction-company.service';
import Swal from 'sweetalert2';
import { CommonModule, NgTemplateOutlet } from '@angular/common';
import { ConstructionCompanyAdmin } from '../../models/ConstructionCompanyAdmin';

@Component({
  selector: 'app-construction-company',
  standalone: true,
  imports: [CommonModule, NgTemplateOutlet],
  templateUrl: './construction-company.component.html',
  styleUrl: './construction-company.component.css'
})
export class ConstructionCompanyComponent {

  constructor(public constructionCompanyService: ConstructionCompanyService) { }

  user: User = {} as User;
  company: ConstructionCompany | null = null;
  token: string = "";

  ngOnInit(): void {
    this.getCompany();
  }

  getCompany(): void {
    this.constructionCompanyService.getConstructionCompanyByUser().subscribe(
      company => this.company = company,
      error => console.log('Error fetching company', error)
    );
  }

  createCompany(): void {
    Swal.fire({
      title: 'Ingrese el nombre de la nueva empresa constructora',
      input: 'text',
      showCancelButton: true,
      confirmButtonText: 'Crear',
      cancelButtonText: 'Cancelar',
    }).then(result => {
      if (result.isConfirmed && result.value) {
        console.log('Creando empresa constructora', result.value);
        this.company = {} as ConstructionCompany;
        this.company.name = result.value;
        this.constructionCompanyService.createConstructionCompany(this.company).subscribe(
          company => {
            this.company = company;
            Swal.fire({
              title: 'Empresa creada',
              text: 'La empresa constructora ha sido creada exitosamente.',
              icon: 'success',
              confirmButtonText: 'Aceptar'
            });
          },
          error => {
            Swal.fire({
              title: 'Error',
              text: error.error.errorMessage || 'Hubo un problema al crear la empresa constructora.',
              icon: 'error',
              confirmButtonText: 'Aceptar'
            });
            console.log('Error creando la empresa', error);
          }
        );
      }
    });
  }  

  modifyCompanyName(): void {
    Swal.fire({
      title: 'Modificar el nombre de la empresa constructora',
      input: 'text',
      inputValue: this.company!.name,
      showCancelButton: true,
      confirmButtonText: 'Guardar',
      cancelButtonText: 'Cancelar',
    }).then(result => {
      if (result.isConfirmed && result.value) {
        let name = this.company!.name;
        this.company!.name = result.value;
        this.constructionCompanyService.modifyConstructionCompanyName(this.company!).subscribe(
          company => {
            this.company = company;
            Swal.fire({
              title: 'Nombre modificado',
              text: 'El nombre de la empresa ha sido modificado exitosamente.',
              icon: 'success',
              confirmButtonText: 'Aceptar'
            });
          },
          error => {
            this.company!.name = name;
            Swal.fire({
              title: 'Error',
              text: error.error.errorMessage || 'Hubo un problema al modificar el nombre de la empresa.',
              icon: 'error',
              confirmButtonText: 'Aceptar'
            });
            console.log('Error modificando el nombre', error);
          }
        );
      }
    });
  }  

  createConstructionCompanyAdmin(): void {
    Swal.fire({
      title: 'Crear y unir administrador a la empresa constructora',
      html: `
      <div class="flex flex-col">
        <div>
          <input type="text" id="name" class="swal2-input" placeholder="Nombre"><span class="requiredDataForm">*</span>
        </div>
        <div>
          <input type="text" id="lastName" class="swal2-input" placeholder="Apellido">
        </div>
        <div>
          <input type="email" id="email" class="swal2-input" placeholder="Email"><span class="requiredDataForm">*</span>
        </div>
        <div>
          <input type="password" id="password" class="swal2-input" placeholder="Contraseña"><span class="requiredDataForm">*</span>
        </div>
        <div class="text-sm text-gray-500 mt-2">
          Datos requeridos: <span class="requiredDataForm">*</span>
        </div>
      </div>
      `,
      focusConfirm: false,
      showCancelButton: true,
      confirmButtonText: 'Crear usuario',
      cancelButtonText: 'Cancelar',
      preConfirm: () => {
        const name = (Swal.getPopup()?.querySelector('#name') as HTMLInputElement)?.value;
        const lastName = (Swal.getPopup()?.querySelector('#lastName') as HTMLInputElement)?.value;
        const email = (Swal.getPopup()?.querySelector('#email') as HTMLInputElement)?.value;
        const password = (Swal.getPopup()?.querySelector('#password') as HTMLInputElement)?.value;
  
        if (!name || !email || !password) {
          Swal.showValidationMessage('Por favor ingrese todos los datos');
          return null;
        }
        return { name, lastName, email, password };
      }
    }).then((result) => {
      if (result.isConfirmed && result.value) {
        const ccadmin: ConstructionCompanyAdmin = {
          email: result.value.email,
          name: result.value.name,
          lastName: result.value.lastName,
          password: result.value.password
        };
        this.constructionCompanyService.inviteConstructionCompanyAdmin(ccadmin).subscribe(
          () => {
            Swal.fire({
              title: 'Usuario creado',
              text: 'El usuario ha sido invitado exitosamente.',
              icon: 'success',
              confirmButtonText: 'Aceptar'
            });
          },
          error => {
            Swal.fire({
              title: 'Error',
              text: error.error.errorMessage || 'Hubo un problema al invitar al usuario.',
              icon: 'error',
              confirmButtonText: 'Aceptar'
            });
            console.log('Error invitando usuario', error);
          }
        );
      }
    });
  }  
  
}
