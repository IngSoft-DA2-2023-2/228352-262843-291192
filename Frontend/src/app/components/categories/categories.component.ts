import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../models/Category';
import Swal from 'sweetalert2';
import * as bootstrap from 'bootstrap';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateCategory } from '../../models/CreateCategory';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [NgFor, HttpClientModule, CommonModule, FormsModule, NgIf, ReactiveFormsModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css',
  providers: [CategoryService]
})
export class CategoriesComponent implements OnInit {
  constructor(public routerServices: Router, public categoryService: CategoryService, private fb: FormBuilder) {
    this.newCategoryForm = this.fb.group({
      name: [''],

    });
  }

  categories: Category[] = [];
  chosenCategory: Category | undefined;
  error: string = "";
  parentCategory: string = "";
  modal: bootstrap.Modal | undefined;
  newCategoryForm: FormGroup;

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

  getParentCandidates() {
    var categories: Category[] = []
    if (this.chosenCategory != null) {
      for (let category of this.categories) {
        if (category.id != this.chosenCategory?.id && category.parentId != this.chosenCategory?.id) {
          categories.push(category);
        }
      }
    }
    return categories
  }

  openNewCategoryModal(): void {
    const modalElement = document.getElementById('newCategoryModal');
    if (modalElement) {
      this.modal = new bootstrap.Modal(modalElement);
      this.modal.show();
    }
  }

  onSubmit(): void {
    this.error = "";
    if (this.newCategoryForm.value.name != "") {
      const newRequest: CreateCategory = this.newCategoryForm.value;
      var categoriesObservable: Observable<Category> | null = this.categoryService.createCategory(newRequest);
      if (categoriesObservable != null) {
        categoriesObservable.subscribe(
          response => {
            this.categories.push(response);
            this.modal?.hide();
            this.closeNewCategoryModal();
          },
          (error: any) => {
            this.error = error.error.errorMessage;
          }
        );

      } else {
        this.error = "Error al crear la categoria";
      }
    }
    else {
      this.error = "Por favor llene todos los campos";
    }
  }

  closeNewCategoryModal() {
    this.newCategoryForm = this.fb.group({
      name: ['']
    });
    this.error = "";
  }

}
