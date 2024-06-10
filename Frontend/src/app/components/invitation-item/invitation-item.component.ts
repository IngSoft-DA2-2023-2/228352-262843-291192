import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgIf } from '@angular/common';
import { Invitation } from '../../models/Invitation';
import { UserRole } from '../../enums/UserRole';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-invitation-item',
  standalone: true,
  imports: [NgIf],
  templateUrl: './invitation-item.component.html',
  styleUrl: './invitation-item.component.css'
})
export class InvitationItemComponent {
  @Input() rowData!: Invitation;
  @Input() editable!: boolean;
  @Output() save = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();

  onFocus(event: any) {
    event.target.type = 'date';
  }

  onBlur(event: any) {
    if (event.target.value === '') {
      event.target.type = 'text';
      event.target.value = this.formatDate(this.rowData.deadline);
    }
  }

  formatDate(unixTimestamp: number): string {
    return new Date(unixTimestamp * 1000).toLocaleDateString();
  }

  saveRow() {
    const inputName = `new-deadline-${this.rowData.id}`;
    const newDeadlineValue = (document.getElementsByName(inputName)[0] as HTMLInputElement).value;
    if (newDeadlineValue) {
      this.rowData.deadline = new Date(newDeadlineValue).getTime() / 1000;
      this.save.emit(this.rowData);
      Swal.fire({
        title: 'Exito!',
        text: 'Se actualizo el deadline correctamente.',
        icon: 'success'
      });
    } else {
      Swal.fire({
        title: 'Error!',
        text: 'Please enter a valid date.',
        icon: 'error'
      });
    }
  }
  

  deleteRow() {
    Swal.fire({
      title: 'Seguro?',
      text: "No es posible revertirlo!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Si, estoy seguro!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.delete.emit(this.rowData);
        Swal.fire(
          'Eliminado!',
          'La invitacion se ha eliminado.',
          'success'
        );
      }
    });
  }
  

  getRoleString(value: number): string {
    switch(value) {
        case UserRole.MANAGER:
            return 'Encargado';
        case UserRole.CONSTRUCTIONCOMPANYADMIN:
            return 'Admin Constructora';
        default:
            return UserRole[value];
    }
}
}
