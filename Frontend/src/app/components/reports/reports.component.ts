import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../services/report.service';
import { Router } from '@angular/router';
import { BuildingsReport } from '../../models/BuildingsReport';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgFor, NgIf, NgSwitch, NgSwitchCase } from '@angular/common';
import { MaintenancesReport } from '../../models/MaintenancesReport';
import { ApartmentsReport } from '../../models/ApartmentsReport';
import { BuildingService } from '../../services/building.service';
import { ManagerBuildings } from '../../models/ManagerBuilding';
import { UserService } from '../../services/user.service';
import { Maintainers } from '../../models/Maintainers';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css',
  imports: [FormsModule, HttpClientModule, NgFor, NgSwitchCase, NgSwitch, NgIf],
  standalone: true,
  providers: [ReportService, BuildingService, UserService]
})
export class ReportsComponent implements OnInit {
  buildingsReport: BuildingsReport | undefined;
  maintenancesReport: MaintenancesReport | undefined;
  apartmentsReport: ApartmentsReport | undefined;

  reportType: string = '0';
  buildingId: string = "";
  managerId: string = "";
  buildings: ManagerBuildings | undefined;
  buildingFilter: string = "";
  maintainerFilter: string = "";
  openFilters: boolean = false;
  maintainers: Maintainers | undefined;
  errors: string = "";

  constructor(public reportService: ReportService, public routerServices: Router, public buildingsService: BuildingService, public userService: UserService) { }

  ngOnInit(): void {
    let managerConnected = localStorage.getItem('connectedUser');
    if (managerConnected == null) {
      //TODO: Redirect to login
      // this.routerServices.navigate(['/login']);
    }
    else {
      let managerConnectedJson = JSON.parse(managerConnected);
      this.managerId = managerConnectedJson.userId;
    }

    var buildingsObservable: Observable<ManagerBuildings> | null = this.buildingsService.getManagerBuildings(this.managerId);
    if (buildingsObservable != null) {
      buildingsObservable.subscribe(buildings => {
        this.buildings = buildings;
      });
    }

    var usersObservable: Observable<Maintainers> | null = this.userService.getMaintenanceStaff();
    if (usersObservable != null) {
      usersObservable.subscribe(maintainers => {
        this.maintainers = maintainers;
      });
    }
  }


  getReport() {
    console.log(this.reportType)
    switch (this.reportType) {
      case "0":
        var buildingsReportsObservable: Observable<BuildingsReport> | null = null;
        buildingsReportsObservable = this.reportService.getBuildingsReport(this.buildingFilter);
        if (buildingsReportsObservable != null) {
          buildingsReportsObservable.subscribe(report => {
            this.buildingsReport = report;
          });
        }
        break;
      case "1":
        var maintenancesReportsObservable: Observable<MaintenancesReport> | null = null;
        if (this.buildingId !== "") {
          this.errors = "";
          maintenancesReportsObservable = this.reportService.getMaintenanceReport(this.buildingId, this.maintainerFilter);
          if (maintenancesReportsObservable != null) {
            maintenancesReportsObservable.subscribe(report => {
              this.maintenancesReport = report;
            });
          }
        } else {
          this.errors = "Por favor seleccione un edificio";
        }
        break;
      case "2":
        var apartmentsReportsObservable: Observable<ApartmentsReport> | null = null;
        if (this.buildingId !== "") {
          this.errors = "";
          apartmentsReportsObservable = this.reportService.getApartmentsReport(this.buildingId);
          if (apartmentsReportsObservable != null) {
            apartmentsReportsObservable.subscribe(report => {
              this.apartmentsReport = report;
            });
          }
        } else {
          this.errors = "Por favor seleccione un edificio";
        }
        break;
    }
  }

  onReportTypeChange(event: Event) {
    this.errors = "";
    const selectElement = event.target as HTMLSelectElement;
    this.reportType = selectElement.value;
  }

  onBuildingIdChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.buildingId = selectElement.value;
  }

  onBuildingFilterChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.buildingFilter = selectElement.value;
  }

  onMaintainerFilterChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.maintainerFilter = selectElement.value;
  }

  changeFilters() {
    this.openFilters = !this.openFilters;
  }
}
