import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RequestService } from '../../services/request.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgFor } from '@angular/common';
import { ManagerRequest } from '../../models/ManagerRequest';

@Component({
  selector: 'app-requests',
  standalone: true,
  imports: [FormsModule, HttpClientModule, NgFor],
  templateUrl: './requests.component.html',
  styleUrl: './requests.component.css',
  providers: [RequestService]
})
export class RequestsComponent {
  constructor(public requestService: RequestService, public routerServices: Router) { }

  requests: ManagerRequest[] = [];
  ngOnInit(): void {
    var requestsObservable: Observable<ManagerRequest[]> | null = this.requestService.getManagerRequests();
    if (requestsObservable != null) {
      requestsObservable.subscribe(requests => {
        this.requests = requests;
      });
    }
  }
}
