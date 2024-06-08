import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RequestService } from '../../services/request.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgFor, NgIf } from '@angular/common';
import { ManagerRequest } from '../../models/ManagerRequest';
import { RequestState } from '../../models/RequestState';
import * as bootstrap from 'bootstrap';
import { Maintainers } from '../../models/Maintainers';
import { UserService } from '../../services/user.service';
import { ListRequests } from '../../models/ListRequests';

@Component({
  selector: 'app-requests',
  standalone: true,
  imports: [FormsModule, HttpClientModule, NgFor, NgIf],
  templateUrl: './requests.component.html',
  styleUrl: './requests.component.css',
  providers: [RequestService, UserService]
})
export class RequestsComponent {
  constructor(public requestService: RequestService, public routerServices: Router, public userService: UserService) { }
  openRequests: ManagerRequest[] = [];
  closeRequests: ManagerRequest[] = [];
  pendingRequests: ManagerRequest[] = [];
  requestsView: string = "0";
  maintainers: Maintainers | undefined;
  chosenMaintainer: string = "";
  chosenRequest: ManagerRequest | undefined;
  error: string = "";
  modal: bootstrap.Modal | undefined;
  filter: string = "";
  openFilters: boolean = false;

  ngOnInit(): void {
    var requestsObservable: Observable<ListRequests> | null = this.requestService.getManagerRequests();
    if (requestsObservable != null) {
      requestsObservable.subscribe(requests => {
        for (var request of requests.requests) {
          if (request.state == RequestState.OPEN) {
            this.openRequests.push(request);
          } else if (request.state == RequestState.CLOSE) {
            this.closeRequests.push(request);
          } else this.pendingRequests.push(request);
        }
      });
    }

    var usersObservable: Observable<Maintainers> | null = this.userService.getMaintenanceStaff();
    if (usersObservable != null) {
      usersObservable.subscribe(maintainers => {
        this.maintainers = maintainers;
      });
    }
  }

  onRequestsViewChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.requestsView = selectElement.value;
  }

  openAssignOpenRequestModal(id: string): void {
    const modalElement = document.getElementById('assignOpenRequestModal');
    if (modalElement) {
      this.chosenRequest = this.openRequests.find(request => request.id == id);
      this.chosenMaintainer = this.chosenRequest?.maintainerStaffId !== '00000000-0000-0000-0000-000000000000' && this.chosenRequest?.maintainerStaffId != null ? this.chosenRequest?.maintainerStaffId : "";
      this.modal = new bootstrap.Modal(modalElement);
      this.modal.show();
    }
  }

  onCloseAssignOpenRequestModal(): void {
    this.chosenRequest = undefined;
    this.chosenMaintainer = "";
    this.error = "";
  }

  onMaintainerChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.chosenMaintainer = selectElement.value;
  }

  onAssignSave(): void {
    if (this.chosenRequest != null && this.chosenMaintainer !== "") {
      this.error = "";
      var requestsObservable: Observable<ManagerRequest> | null = this.requestService.assignRequest(this.chosenRequest.id, this.chosenMaintainer);
      if (requestsObservable != null) {
        requestsObservable.subscribe(request => {
          var oldRequest: ManagerRequest | undefined = this.openRequests.find(request => request.id == this.chosenRequest?.id);
          if (oldRequest == null) {
            this.error = "Error al asignar la solicitud";
          } else {
            oldRequest.maintainerStaffId = request.maintainerStaffId;
            oldRequest.maintainerStaffName = request.maintainerStaffName;
            this.modal?.hide();
            this.onCloseAssignOpenRequestModal();
          }
        });
      }
    } else {
      this.error = "Seleccione una persona de mantenimiento";
    }
  }

  onFilterChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.filter = selectElement.value;
  }

  changeFilters() {
    this.openFilters = !this.openFilters;
  }
}
