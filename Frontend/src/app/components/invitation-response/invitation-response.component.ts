import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { InvitationService } from '../../services/invitation.service';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Invitation } from '../../models/Invitation';

export interface InvitationResponse {
  invitations: Invitation[];
}
@Component({
  selector: 'app-invitation-response',
  standalone: true,
  imports: [RouterOutlet, ReactiveFormsModule, RouterLink],
  templateUrl: './invitation-response.component.html',
  styleUrls: ['./invitation-response.component.css']
})
export class InvitationResponseComponent {
  acceptForm: FormGroup;
  declineForm: FormGroup;

  private errorMessages: { [key: string]: string } = {
    "Invitation not found.": "Invitación no encontrada.",
    "Invitation expired.": "La invitación ha expirado.",
    "Invitation was accepted.": "La invitación ya fue aceptada.",
    "Invitation was rejected.": "La invitación ya fue rechazada."
  };

  constructor(
    private fb: FormBuilder,
    private invitationService: InvitationService
  ) {
    this.acceptForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });

    this.declineForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onAccept() {
    if (this.acceptForm.valid) {
      const { email, password } = this.acceptForm.value;
      
      this.invitationService.getInvitationByEmail(email).subscribe(
        (invitationResponse) => {
          const response = invitationResponse as unknown as InvitationResponse;
          const invitation = response.invitations[0];
          const id = invitation.id;

          this.invitationService.respondToInvitation(id??"", email, password, 0).subscribe(
            response => {
              Swal.fire({
                icon: 'success',
                title: 'Invitación aceptada',
                text: 'La invitación ha sido aceptada exitosamente.',
              });
              this.acceptForm.reset();
            },
            error => {
              console.error('Error:', error);
              Swal.fire({
                icon: 'error',
                title: 'Error',
                text: error.error.errorMessage || 'Ocurrió un error al aceptar la invitación.',
              });
              this.acceptForm.reset();
            }
          );
        },
        error => {
          const errorMessage = this.errorMessages[error.error.errorMessage] || 'Ocurrió un error al crear la invitación.';
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: errorMessage || 'Ocurrió un error al rechazar la invitación.',
            });
        }
      );
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Campos incompletos',
        text: 'Por favor, completa todos los campos del formulario correctamente.',
      });
    }
  }

  onDecline() {
    if (this.declineForm.valid) {
      const { email } = this.declineForm.value;

      this.invitationService.getInvitationByEmail(email).subscribe(
        (invitationResponse) => {
          const response = invitationResponse as unknown as InvitationResponse;
          const invitation = response.invitations[0];
          const id = invitation.id;
          console.log('Invitation:', invitationResponse);
          
          this.invitationService.respondToInvitation(id??"", email, '', 1).subscribe(
            response => {
              Swal.fire({
                icon: 'success',
                title: 'Invitación rechazada',
                text: 'La invitación ha sido rechazada exitosamente.',
              });
              this.declineForm.reset();
              
            },
            error => {
              const errorMessage = this.errorMessages[error.error.errorMessage] || 'Ocurrió un error al crear la invitación.';
              Swal.fire({
                icon: 'error',
                title: 'Error',
                text: errorMessage || 'Ocurrió un error al rechazar la invitación.',
              });
              this.declineForm.reset();
            }
          );
        },
        error => {
          console.error('Error fetching invitation:', error);
          if (error.status === 404) {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'No se encontró una invitación para este correo electrónico.',
            });
          } else if (error.status === 400) { 
            console.log('Error:', error);
            
            Swal.fire({
              icon: 'error',
              title: 'Invitación expirada',
              text: 'La invitación ha expirado y ya no es válida.',
            });
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'Ocurrió un error al buscar la invitación.',
            });
          }
        }
      );
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Campos incompletos',
        text: 'Por favor, completa todos los campos del formulario correctamente.',
      });
    }
  }
}
