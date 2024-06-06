import { Component } from '@angular/core';
import { ReportsService } from '../../services/reports.service';
import { Router } from '@angular/router';
import { BuildingsReport } from '../../models/BuildingsReport';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgFor, NgSwitch, NgSwitchCase } from '@angular/common';
import { MaintenancesReport } from '../../models/MaintenancesReport';
import { ApartmentsReport } from '../../models/ApartmentsReport';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css',
  imports: [FormsModule, HttpClientModule, NgFor, NgSwitchCase, NgSwitch],
  standalone: true,
  providers: [ReportsService]
})
export class ReportsComponent {
  buildingsReport: BuildingsReport | undefined;
  maintenancesReport: MaintenancesReport | undefined;
  apartmentsReport: ApartmentsReport | undefined;

  reportType: string = '0';
  buildingId: string = "";
  constructor(public reportService: ReportsService, public routerServices: Router) { }


  getReport() {
    console.log(this.reportType)
    switch (this.reportType) {
      case "0":
        var buildingsReportsObservable: Observable<BuildingsReport> | null = null;
        buildingsReportsObservable = this.reportService.getBuildingsReport();
        if (buildingsReportsObservable != null) {
          buildingsReportsObservable.subscribe(report => {
            this.buildingsReport = report;
          });
        }
        break;
      case "1":
        var maintenancesReportsObservable: Observable<MaintenancesReport> | null = null;
        this.buildingId = '432B0F52-7B8B-48D8-DB1D-08DC80D8B8D6';
        maintenancesReportsObservable = this.reportService.getMaintenanceReport(this.buildingId);
        if (maintenancesReportsObservable != null) {
          maintenancesReportsObservable.subscribe(report => {
            this.maintenancesReport = report;
          });
        }
        break;
      case "2":
        var apartmentsReportsObservable: Observable<ApartmentsReport> | null = null;
        this.buildingId = '432B0F52-7B8B-48D8-DB1D-08DC80D8B8D6';
        apartmentsReportsObservable = this.reportService.getApartmentsReport(this.buildingId);
        if (apartmentsReportsObservable != null) {
          apartmentsReportsObservable.subscribe(report => {
            this.apartmentsReport = report;
          });
        }
        break;
    }
  }

  onReportTypeChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.reportType = selectElement.value;
  }
}
