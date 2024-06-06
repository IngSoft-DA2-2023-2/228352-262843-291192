import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BuildingDetails } from '../../models/BuildingDetails';
import { BuildingService } from '../../services/building.service';

@Component({
  selector: 'app-building-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule],
  templateUrl: './building-detail.component.html',
  styleUrl: './building-detail.component.css'
})
export class BuildingDetailComponent {
  buildingName: string = '';
  building: BuildingDetails = {} as BuildingDetails;

  constructor(private route: ActivatedRoute, public buildingService: BuildingService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const encodedBuildingName = params.get('id');
      if (encodedBuildingName) {
        this.buildingName = decodeURIComponent(encodedBuildingName);
        this.buildingService.getBuildingDetails(this.buildingName).subscribe(
          (response: any) => {
            this.building = response;
            console.log('Edificio:', this.building);
          },
          (error: any) => {
            console.log('Error al obtener el edificio:', error);
          }
        );
      }
    });
  }
}
