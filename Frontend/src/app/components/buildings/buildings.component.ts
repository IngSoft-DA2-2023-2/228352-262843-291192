import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Building } from '../../models/Building';
import { BuildingService } from '../../services/building.service';
import { HttpClientModule } from '@angular/common/http';

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
}
