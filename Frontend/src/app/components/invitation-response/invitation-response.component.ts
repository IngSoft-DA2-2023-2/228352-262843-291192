import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { InvitationService } from '../../services/invitation.service';
import { RouterLink, RouterOutlet } from '@angular/router';

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
          const invitation = invitationResponse.invitations[0];
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
          console.error('Error fetching invitation:', error);
          if (error.status === 404) {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'No se encontró una invitación para este correo electrónico.',
            });
          } else if (error.status === 410) {
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

  onDecline() {
    if (this.declineForm.valid) {
      const { email } = this.declineForm.value;

      this.invitationService.getInvitationByEmail(email).subscribe(
        (invitationResponse) => {
          const invitation = invitationResponse.invitations[0];
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
              console.error('Error:', error);
              Swal.fire({
                icon: 'error',
                title: 'Error',
                text: error.error.errorMessage || 'Ocurrió un error al rechazar la invitación.',
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
