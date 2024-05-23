import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { authGuard } from './guards/auth.guard';
import { authRedirectGuard } from './guards/auth-redirect.guard';

export const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [authGuard], pathMatch: 'full'},
  { path: 'login', component: LoginComponent, canActivate: [authRedirectGuard], pathMatch: 'full'},
  { path: '**', redirectTo: '/login' }
];
