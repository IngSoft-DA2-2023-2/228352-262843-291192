import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-importer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './importer.component.html',
  styleUrls: ['./importer.component.css']
})
export class ImporterComponent {
  isDragOver: boolean = false;
  selectedImporter: string = '';
  importers: string[] = ['Importador JSON', 'Importador XML'];

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

    const formData = new FormData();
    formData.append('file', file);
    formData.append('importer', this.selectedImporter);
    console.log('Archivo:', file);
    /*
    fetch('URL_DEL_BACKEND', {
      method: 'POST',
      body: formData
    }).then(response => response.json())
      .then(data => {
        if (data.success) {
          Swal.fire({
            title: 'Archivo cargado',
            text: 'El archivo se ha procesado correctamente.',
            icon: 'success',
            confirmButtonText: 'OK'
          });
        } else {
          Swal.fire({
            title: 'Error',
            text: data.message,
            icon: 'error',
            confirmButtonText: 'OK'
          });
        }
      }).catch(error => {
        Swal.fire({
          title: 'Error',
          text: 'Hubo un problema al procesar el archivo.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
      });*/
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