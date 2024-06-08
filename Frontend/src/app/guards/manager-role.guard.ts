import { CanActivateFn } from '@angular/router';
import { UserRole } from '../models/UserRole';

export const managerRoleGuard: CanActivateFn = (route, state) => {
  const connectedUser = JSON.parse(localStorage.getItem('connectedUser') || '{}');
  const role = connectedUser.role;
  const managerRole = UserRole.MANAGER;

  if (role && role == managerRole) {
    return true;
  }
  
  window.location.href = '/manager/home';
  return false;
};