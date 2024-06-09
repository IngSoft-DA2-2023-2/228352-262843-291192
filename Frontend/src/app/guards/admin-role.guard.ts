import { CanActivateFn } from '@angular/router';
import { UserRole } from '../enums/UserRole';
import { User } from '../models/User';

export const adminRoleGuard: CanActivateFn = (route, state) => {
  const connectedUser = JSON.parse(localStorage.getItem('connectedUser') as string) as User;
  
  if (connectedUser.role ==  UserRole.ADMIN) {
    return true;
  }

  window.location.href = '/manager/home';
  return false;
};
