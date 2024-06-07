import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BuildingDetails } from '../../models/BuildingDetails';
import { BuildingService } from '../../services/building.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-building-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './building-detail.component.html',
  styleUrl: './building-detail.component.css'
})
export class BuildingDetailComponent {
  building: BuildingDetails = {} as BuildingDetails;
  editBuildingForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public buildingService: BuildingService
  ) {
    this.editBuildingForm = this.fb.group({
      name: [''],
      address: [''],
      location: [''],
      commonExpenses: [''],
      apartments: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const buildingId = params.get('id');
      if (buildingId) {
        this.buildingService.getBuildingDetails(buildingId).subscribe(
          (response: any) => {
            this.building = response;
            this.editBuildingForm.patchValue(this.building);
            this.setApartments(this.building.apartments);
          },
          (error: any) => {
            console.log('Error al obtener el edificio:', error);
          }
        );
      }
    });
  }

  setApartments(apartments: any[]): void {
    const apartmentFGs = apartments.map(apartment => this.fb.group({
      number: [apartment.number],
      floor: [apartment.floor],
      rooms: [apartment.rooms],
      bathrooms: [apartment.bathrooms],
      hasTerrace: [apartment.hasTerrace],
      owner: this.fb.group({
        name: [apartment.owner.name],
        lastName: [apartment.owner.lastName],
        email: [apartment.owner.email]
      })
    }));
    const apartmentFormArray = this.fb.array(apartmentFGs);
    this.editBuildingForm.setControl('apartments', apartmentFormArray);
  }

  openEditBuildingModal(): void {
    const modalElement = document.getElementById('editBuildingModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  onSubmit(): void {
    if (this.editBuildingForm.valid) {
      const updatedBuilding = { ...this.building, ...this.editBuildingForm.value };
      this.buildingService.updateBuilding(updatedBuilding.id, updatedBuilding).subscribe(
        (response: any) => {
          this.building = response;
          const modalElement = document.getElementById('editBuildingModal');
          if (modalElement) {
            const modal = bootstrap.Modal.getInstance(modalElement);
            modal?.hide();
          }
        },
        (error: any) => {
          console.log('Error al actualizar el edificio:', error);
        }
      );
    }
  }

}
