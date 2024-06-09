import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Maintainer, Maintainers } from '../../models/Maintainers';
import { UserService } from '../../services/user.service';
import { NgFor } from '@angular/common';
import * as bootstrap from 'bootstrap';
import { FormGroup, FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CreateMaintainer } from '../../models/CreateMaintainer';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-maintainers',
  standalone: true,
  imports: [NgFor, ReactiveFormsModule, FormsModule, HttpClientModule],
  templateUrl: './maintainers.component.html',
  styleUrl: './maintainers.component.css',
  providers: [UserService]

})
export class MaintainersComponent implements OnInit {

  maintainers: Maintainers | undefined;
  newMaintainerForm: FormGroup;
  error: string = "";

  constructor(public routerServices: Router, public userService: UserService, private fb: FormBuilder) {
    this.newMaintainerForm = this.fb.group({
      name: [''],
      lastname: [''],
      email: [''],
      password: ['']
    });
  }

  ngOnInit(): void {
    var usersObservable: Observable<Maintainers> | null = this.userService.getMaintenanceStaff();
    if (usersObservable != null) {
      usersObservable.subscribe(maintainers => {
        this.maintainers = maintainers;
      },
        (error: any) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: error.error.errorMessage || 'Ocurrió un error al cargar los datos de los usuarios de mantenimiento'
          });
        });
    }
  }

  openNewMaintainerModal(): void {
    const modalElement = document.getElementById('newMaintainerModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  onSubmit(): void {
    this.error = "";
    if (this.newMaintainerForm.value.name != "" && this.newMaintainerForm.value.lastname != "" &&
      this.newMaintainerForm.value.email != "" && this.newMaintainerForm.value.password != "") {
      const newMaintainer: CreateMaintainer = this.newMaintainerForm.value;
      var maintenancesObservable: Observable<Maintainer> | null = this.userService.createMaintainer(newMaintainer);
      if (maintenancesObservable != null) {
        maintenancesObservable.subscribe(
          response => {
            this.maintainers?.maintainers.push(response);
            const modalElement = document.getElementById('newMaintainerModal');
            if (modalElement) {
              const modal = bootstrap.Modal.getInstance(modalElement);
              modal?.hide();
            }
          },
          (error: any) => {
            this.error = error.error.errorMessage;
          }
        );
      }
    } else {
      this.error = "Por favor llene todos los campos";
    }
  }

  closeNewMaintainerModal() {
    this.newMaintainerForm = this.fb.group({
      name: [''],
      lastname: [''],
      email: [''],
      password: ['']
    });
    this.error = "";
  }
}
