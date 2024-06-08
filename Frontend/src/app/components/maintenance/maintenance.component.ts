import { Component } from '@angular/core';
import { ManagerRequest } from '../../models/ManagerRequest';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RequestService } from '../../services/request.service';
import { Observable } from 'rxjs';
import { ListRequests } from '../../models/ListRequests';
import Swal from 'sweetalert2';
import { RequestState } from '../../models/RequestState';

@Component({
  selector: 'app-maintenance',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './maintenance.component.html',
  styleUrl: './maintenance.component.css'
})
export class MaintenanceComponent {
  constructor(private requestService: RequestService) { }
  requestsView: string = "0";
  openRequests: ManagerRequest[] = [];
  pendingRequests: ManagerRequest[] = [];
  closedRequests: ManagerRequest[] = [];

  ngOnInit(): void {
    let requestsObservable: Observable<ListRequests> | null = this.requestService.getAssignedRequests();
    if (requestsObservable != null) {
      requestsObservable.subscribe(requests => {
        for (let request of requests.requests) {
          if (request.state == RequestState.OPEN) {
            this.openRequests.push(request);
          } else if (request.state == RequestState.CLOSE) {
            this.closedRequests.push(request);
          } else {
            this.pendingRequests.push(request);
          }
        }
      });
    }
  }

  onRequestsViewChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.requestsView = selectElement.value;
  }

  onAttendanceRequest(request: ManagerRequest): void {
    Swal.fire({
      title: '¿Está seguro de atender la solicitud?',
      showCancelButton: true,
      confirmButtonText: 'Sí',
      cancelButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        let requestsObservable: Observable<ManagerRequest> | null = this.requestService.attendRequest(request.id);
        if (requestsObservable != null) {
          requestsObservable.subscribe(requestUpdated => {
            requestUpdated.state = RequestState.ATTENDING;
            this.openRequests = this.openRequests.filter(req => req.id !== requestUpdated.id);
            this.pendingRequests.push(requestUpdated);
            Swal.fire('Solicitud atendida', '', 'success');
          });
        }
      } else {
        Swal.fire('Solicitud no atendida', '', 'error');
      }
    });
  }

  onCloseRequest(request: ManagerRequest): void {
    Swal.fire({
      title: 'Ingrese el costo del servicio',
      input: 'number',
      inputLabel: 'Costo',
      inputPlaceholder: 'Ingrese el costo del servicio',
      showCancelButton: true,
      confirmButtonText: 'Guardar',
      cancelButtonText: 'Cancelar',
      inputValidator: (value) => {
        if (!value || Number(value) <= 0) {
          return 'Debe ingresar un costo válido';
        }
        return null;
      }
    }).then((result) => {
      if (result.isConfirmed) {
        const cost = result.value;
        console.log(`Request ID: ${request.id}, Cost: ${cost}`);
        let requestsObservable: Observable<ManagerRequest> | null = this.requestService.closeRequest(request.id, cost);
        if (requestsObservable != null) {
          requestsObservable.subscribe(requestUpdated => {
            requestUpdated.state = RequestState.CLOSE;
            this.pendingRequests = this.pendingRequests.filter(req => req.id !== requestUpdated.id);
            this.closedRequests.push(requestUpdated);
            Swal.fire('Solicitud cerrada', '', 'success');
          });
        }
      }else{
        Swal.fire('Solicitud no cerrada', '', 'error');
      }
    });
  }
  
}