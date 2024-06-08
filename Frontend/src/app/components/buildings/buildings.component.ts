import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Building } from '../../models/Building';
import { BuildingService } from '../../services/building.service';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-buildings',
  standalone: true,
  templateUrl: './buildings.component.html',
  template: ` <building-detail [childMessage]="buildingName"></building-detail> `,
  styleUrl: './buildings.component.css',
  imports: [CommonModule, RouterModule, HttpClientModule],
  providers: [BuildingService]
})
export class BuildingsComponent {
  buildingName: string = '';
  buildings: Building[] = []

  constructor(public buildingService: BuildingService) { 
    this.buildingService.getBuildings().subscribe(
      (response: any) => {
        if (response && Array.isArray(response.buildings)) {
          this.buildings = response.buildings;
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
}
