import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../services/report.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgFor, NgIf, NgSwitch, NgSwitchCase } from '@angular/common';
import { BuildingService } from '../../services/building.service';
import { UserService } from '../../services/user.service';
import { Maintainers } from '../../models/Maintainers';
import Swal from 'sweetalert2';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/Category';
import { Building } from '../../models/Building';
import { CategoriesReport } from '../../models/CategoriesReport';

@Component({
  selector: 'app-admin-reports',
  standalone: true,
  imports: [FormsModule, HttpClientModule, NgFor, NgSwitchCase, NgSwitch, NgIf],
  templateUrl: './admin-reports.component.html',
  styleUrl: './admin-reports.component.css',
  providers: [ReportService, BuildingService, UserService, CategoryService]
})
export class AdminReportsComponent implements OnInit {
  categoriesReport: CategoriesReport | undefined;

  reportType: string = '0';
  buildingId: string = "";
  buildings: Array<Building> = [];
  buildingFilter: string = "";
  categoryFilter: string = "";
  openFilters: boolean = false;
  maintainers: Maintainers | undefined;
  errors: string = "";
  categories: Category[] = [];

  constructor(public reportService: ReportService, public routerServices: Router, public buildingsService: BuildingService,
    public userService: UserService, public categoryService: CategoryService) { }

  ngOnInit(): void {
    var buildingsObservable: Observable<Building[]> | null = this.buildingsService.getBuildings();
    if (buildingsObservable != null) {
      buildingsObservable.subscribe((response: any) => {
        this.buildings = response.buildings;
      },
        (error: any) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: error.error.errorMessage || 'Ocurrió un error al cargar los reportes'
          });
        });
    }

    var categoriesObservable: Observable<Category[]> | null = this.categoryService.getCategories();
    if (categoriesObservable != null) {
      categoriesObservable.subscribe(categories => {
        this.categories = categories;
      },
        (error: any) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: error.error.errorMessage || 'Ocurrió un error al cargar los reportes'
          });
        });
    }
  }


  getReport() {
    var categoriesReportsObservable: Observable<CategoriesReport> | null = null;
    if (this.buildingId !== "") {
      this.errors = "";
      categoriesReportsObservable = this.reportService.getCategoriesReport(this.buildingId, this.categoryFilter);
      if (categoriesReportsObservable != null) {
        categoriesReportsObservable.subscribe(report => {
          this.categoriesReport = report;
        },
          (error: any) => {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: error.error.errorMessage || 'Ocurrió un error al cargar el reporte'
            });
          });
      }
    } else {
      this.errors = "Por favor seleccione un edificio";
    }
  }

  onBuildingIdChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.buildingId = selectElement.value;
  }

  onCategoryFilterChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.categoryFilter = selectElement.value;
  }

  changeFilters() {
    this.openFilters = !this.openFilters;
  }
}


