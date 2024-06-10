import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar.component';
import Swal from 'sweetalert2';
import { InvitationService } from '../../services/invitation.service';
import { Invitation } from '../../models/Invitation';
import { InvitationsListComponent } from '../invitations-list/invitations-list.component';

@Component({
  selector: 'app-invite',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, ReactiveFormsModule, InvitationsListComponent],
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.css']
})
export class InviteComponent {
  invitationForm: FormGroup;

  constructor(private fb: FormBuilder, private invitationService: InvitationService) {
    this.invitationForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      rol: ['', Validators.required],
      deadline: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.invitationForm.valid) {
      const formValue = this.invitationForm.value;

      const roleMap: { [key: string]: number } = {
        'Encargado de edificio': 2,
        'Administrador de constructora': 3
      };

      const deadline = new Date(formValue.deadline).getTime() / 1000;

      const invitation: Invitation = {
        name: formValue.name,
        email: formValue.email,
        deadline: deadline, 
        role: roleMap[formValue.rol]
      };

      this.invitationService.createInvitation(invitation).subscribe(
        response => {
          Swal.fire({
            icon: 'success',
            title: 'Invitación creada',
            text: 'La invitación ha sido creada exitosamente.',
            confirmButtonText: 'Aceptar'
          });
          this.invitationForm.reset()
        },
        error => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: error.error.errorMessage || 'Ocurrió un error al crear la invitación.',
            confirmButtonText: 'Aceptar'
          });
        }
      );
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Campos incompletos',
        text: 'Por favor, completa todos los campos del formulario.',
        confirmButtonText: 'Aceptar'
      });
    }
  }
}
