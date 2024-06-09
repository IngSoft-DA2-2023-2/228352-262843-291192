import { CanActivateFn } from '@angular/router';
import { UserRole } from '../models/UserRole';

export const adminRoleGuard: CanActivateFn = (route, state) => {
  const connectedUser = JSON.parse(localStorage.getItem('connectedUser') || '{}');
  const role = connectedUser.role;
  const adminRole = UserRole.ADMIN;

  if (role != null && role == adminRole) {
    return true;
  }

  window.location.href = '/manager/home';
  return false;
};
