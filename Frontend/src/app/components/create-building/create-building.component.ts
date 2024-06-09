import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BuildingService } from '../../services/building.service';
import { BuildingDetails } from '../../models/BuildingDetails';
import { UserService } from '../../services/user.service';
import { Manager } from '../../models/Manager';
import Swal from 'sweetalert2';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-create-building',
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule, ReactiveFormsModule],
  providers: [BuildingService, UserService],
  templateUrl: './create-building.component.html',
  styleUrls: ['./create-building.component.css']
})
export class CreateBuildingComponent implements OnInit {
  createBuildingForm: FormGroup = {} as FormGroup;
  managers: Manager[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, 
              private buildingService: BuildingService, private managerService: ManagerService) {
    this.createBuildingForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      location: ['', Validators.required],
      commonExpenses: [0, [Validators.required, Validators.min(1)]],
      managerId: [null],
      apartments: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadManagers();
  }

  loadManagers(): void {
    this.managerService.getManagers().subscribe(
      (response: any) => {
        this.managers = response.managers;
        console.log('Managers cargados:', this.managers);
      },
      (error: any) => {
        console.error('Error al cargar los managers:', error);
      }
    );
  }

  get apartments(): FormArray {
    return this.createBuildingForm.get('apartments') as FormArray;
  }

  addApartment(): void {
    const apartmentForm = this.fb.group({
      number: ['', Validators.required],
      floor: ['', Validators.required],
      rooms: ['', Validators.required],
      bathrooms: ['', Validators.required],
      hasTerrace: [false],
      owner: this.fb.group({
        name: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]]
      })
    });

    this.apartments.push(apartmentForm);
  }

  removeApartment(index: number): void {
    this.apartments.removeAt(index);
  }

  onSubmit(): void {
    if (this.createBuildingForm.valid) {
      const newBuilding: BuildingDetails = this.createBuildingForm.value;
      this.buildingService.createBuilding(newBuilding).subscribe(
        (response: BuildingDetails) => {
          this.router.navigate(['/manager/buildings']);
        },
        (error: any) => {
          console.error('Error al crear el edificio:', error);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: error.error.errorMessage || 'Ocurrió un error al crear el edificio, por favor intente nuevamente.'
          });
        }
      );
    } else {
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Por favor, complete todos los campos requeridos antes de crear el edificio.',
      });
    }
  }
}