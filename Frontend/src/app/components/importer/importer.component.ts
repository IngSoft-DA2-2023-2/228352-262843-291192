import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { ImporterService } from '../../services/importer.service';
import { Owner } from '../../models/Owner';
import { OwnerService } from '../../services/owner.service';

@Component({
  selector: 'app-importer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './importer.component.html',
  styleUrls: ['./importer.component.css']
})
export class ImporterComponent {

  constructor(private importerService: ImporterService, private ownerService: OwnerService) { }

  isDragOver: boolean = false;
  selectedImporter: string = '';
  importers: string[] = [];
  newOwner: Owner = {} as Owner;

  ngOnInit(): void {
    this.loadImporters();
  }

  loadImporters(): void {
    this.importerService.getImporters().subscribe(
      (response: any) => {
        if (response && Array.isArray(response.importers)) {
          this.importers = response.importers;
        } else {
          console.error('Los datos recibidos no son válidos:', response);
        }
      },
      (error: any) => {
        console.log('Error al obtener edificios:', error);
      }
    );
  }

  addOwner() {
    if (this.newOwner.name && this.newOwner.lastName && this.newOwner.email) {
      this.ownerService.addOwner(this.newOwner).subscribe(
        (response: Owner) => {
          Swal.fire({
            title: 'Propietario agregado',
            text: 'El propietario ' + response.name + ' se ha agregado correctamente.',
            icon: 'success',
            confirmButtonText: 'OK'
          });
          this.newOwner = {} as Owner;
        },
        (error: any) => {
          Swal.fire({
            title: 'Error',
            text: error.error?.errorMessage || 'Hubo un problema al agregar el propietario.',
            icon: 'error',
            confirmButtonText: 'OK'
          });
        }
      );
    } else {
      Swal.fire({
        title: 'Datos incompletos',
        text: 'Por favor, completa todos los campos del propietario.',
        icon: 'warning',
        confirmButtonText: 'OK'
      });
    }
  }

  onFileDropped(event: any) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragOver = false;

    const file = event.dataTransfer.files[0];
    this.uploadFile(file);
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    this.uploadFile(file);
    event.target.value = '';
  }

  uploadFile(file: File) {
    if (!this.selectedImporter) {
      Swal.fire({
        title: 'Importador no seleccionado',
        text: 'Por favor, selecciona un importador antes de subir un archivo.',
        icon: 'warning',
        confirmButtonText: 'OK'
      });
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      const buildingsData = reader.result as string;
      this.importerService.importData(this.selectedImporter, buildingsData).subscribe(
        (response: any) => {
          Swal.fire({
            title: 'Archivo cargado',
            text: 'El archivo se ha procesado correctamente.',
            icon: 'success',
            confirmButtonText: 'OK'
          });
        },
        (error: any) => {
          Swal.fire({
            title: 'Error',
            text: error.error?.errorMessage || 'Hubo un problema al procesar el archivo.',
            icon: 'error',
            confirmButtonText: 'OK'
          });
        }
      );
    };
    reader.readAsText(file);
  }

  preventDefault(event: Event) {
    event.preventDefault();
    event.stopPropagation();
  }

  onDragOver(event: Event) {
    this.preventDefault(event);
    this.isDragOver = true;
  }
  
  onDragLeave(event: Event) {
    this.preventDefault(event);
    this.isDragOver = false;
  }
}