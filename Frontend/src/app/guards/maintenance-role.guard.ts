import { CanActivateFn } from '@angular/router';
import { UserRole } from '../enums/UserRole';

export const maintenanceRoleGuard: CanActivateFn = (route, state) => {
  const connectedUser = JSON.parse(localStorage.getItem('connectedUser') || '{}');
  const role = connectedUser.role;
  const maintenanceRole = UserRole.MAINTENANCE;

  if (role && role == maintenanceRole) {
    return true;
  }
  
  window.location.href = '/manager/home';
  return false;
};
