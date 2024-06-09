import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../models/Category';
import Swal from 'sweetalert2';
import * as bootstrap from 'bootstrap';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [NgFor, HttpClientModule, CommonModule, FormsModule, NgIf],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css',
  providers: [CategoryService]
})
export class CategoriesComponent implements OnInit {
  constructor(public routerServices: Router, public categoryService: CategoryService) { }

  categories: Category[] = [];
  chosenCategory: Category | undefined;
  error: string = "";
  parentCategory: string = "";
  modal: bootstrap.Modal | undefined;

  ngOnInit(): void {
    var categoriesObservable: Observable<Category[]> | null = this.categoryService.getCategories();
    if (categoriesObservable != null) {
      categoriesObservable.subscribe(categories => {
        this.categories = categories;
      },
        (error: any) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: error.error.errorMessage || 'Ocurrió un error al cargar las categorias'
          });
        });
    }
  }

  openAssignParentModal(id: string): void {
    const modalElement = document.getElementById('assignParentModal');
    if (modalElement) {
      this.chosenCategory = this.categories.find(category => category.id == id);
      this.modal = new bootstrap.Modal(modalElement);
      this.modal.show();
    }
  }

  assignParent(): void {
    if (this.chosenCategory != null && this.parentCategory != "") {
      this.error = "";
      var categoriesObservable: Observable<Category> | null = this.categoryService.assignParent(this.chosenCategory.id, this.parentCategory);
      if (categoriesObservable != null) {
        categoriesObservable.subscribe(category => {
          var oldCategory: Category | undefined = this.categories.find(c => c.id == this.chosenCategory?.id);
          if (oldCategory == null) {
            this.error = "Error al vincular categorias";
          } else {
            oldCategory.parentId = category.parentId;
            this.modal?.hide();
            this.closeAssignParentModal();
          }
        },
          (error: any) => {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: error.error.errorMessage || 'Ocurrió un error al vincular las categorias'
            });
          });
      }
    } else {
      this.error = "Seleccione una categoria padre";
    }
  }

  closeAssignParentModal() {
    this.modal?.hide();
    this.chosenCategory = undefined;
    this.error = "";
  }

  onParentCategoryChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.parentCategory = selectElement.value;
  }

}
