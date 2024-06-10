import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Building } from '../../models/Building';
import { BuildingService } from '../../services/building.service';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';
import { ConstructionCompanyService } from '../../services/construction-company.service';
import { ManagerService } from '../../services/manager.service';
import { Manager } from '../../models/Manager';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-buildings',
  standalone: true,
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css'],
  imports: [CommonModule, RouterModule, HttpClientModule, FormsModule, ReactiveFormsModule],
  providers: [BuildingService, ManagerService]
})
export class BuildingsComponent {
  buildingName: string = '';
  buildings: Building[] = [];
  managers: Manager[] = [];
  selectedBuildingId: string = '';
  selectedManagerId: string = '';
  isAddManagerModalOpen: boolean = false;

  constructor(
    public buildingService: BuildingService,
    public constructionCompanyService: ConstructionCompanyService,
    public managerService: ManagerService
  ) {
    const sessionData = localStorage.getItem('connectedUser');
    const ccadminId = sessionData ? JSON.parse(sessionData).userId : null;
    this.constructionCompanyService.getBuildingsFromCCAdmin(ccadminId).subscribe(
      (response: any) => {
        if (response && Array.isArray(response.buildings)) {
          this.buildings = response.buildings;
          console.log('Edificios obtenidos:', this.buildings);
        } else {
          console.error('Los datos recibidos no son válidos:', response);
        }
      },
      (error: any) => {
        console.log('Error al obtener edificios:', error);
      }
    );
  }

  ngOnInit(): void {
    this.managerService.getManagers().subscribe(
      (response: any) => {
        if (response && Array.isArray(response.managers)) {
          this.managers = response.managers;
          console.log('Managers:', this.managers);
        } else {
          console.error('Los datos recibidos no son válidos:', response);
        }
      },
      (error: any) => {
        console.log('Error al obtener edificios:', error);
      }
    );
  }

  openDeleteBuildingModal(buildingId: string): void {
    Swal.fire({
      title: '¿Estás seguro de que quieres eliminar este edificio?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.buildingService.deleteBuilding(buildingId).subscribe(
          () => {
            this.buildings = this.buildings.filter(building => building.id !== buildingId);
            Swal.fire('¡Eliminado!', 'El edificio ha sido eliminado correctamente.', 'success');
          },
          (error: any) => {
            console.error('Error al eliminar el edificio:', error);
            Swal.fire('Error', 'Hubo un error al intentar eliminar el edificio.', 'error');
          }
        );
      }
    });
  }

  openAddManagerModal(buildingId: string): void {
    this.selectedBuildingId = buildingId;
    this.isAddManagerModalOpen = true;
  }

  closeAddManagerModal(): void {
    this.isAddManagerModalOpen = false;
  }

  assignManager(): void {
    this.buildingService.updateBuildingManager(this.selectedBuildingId, this.selectedManagerId).subscribe(
      (response) => {
        this.closeAddManagerModal();
        Swal.fire('¡Manager agregado!', 'El manager ha sido asignado correctamente.', 'success');
        const updatedBuilding = this.buildings.find(building => building.id === this.selectedBuildingId);
        if (updatedBuilding) {
          const manager = this.managers.find(manager => manager.id === this.selectedManagerId);
          if (manager) {
            updatedBuilding.manager = manager.name;
          }
        }
      },
      (error) => {
        Swal.fire('Error', 'Hubo un error al intentar agregar el manager.', 'error');
      }
    );
  }
}