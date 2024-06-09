import { CanActivateFn } from '@angular/router';
import { UserRole } from '../enums/UserRole';

export const constructionCompanyAdminRoleGuard: CanActivateFn = (route, state) => {
  const connectedUser = JSON.parse(localStorage.getItem('connectedUser') || '{}');
  const role = connectedUser.role;
  const constructionCompanyAdminRole = UserRole.CONSTRUCTIONCOMPANYADMIN;

  if (role && role == constructionCompanyAdminRole) {
    return true;
  }

  window.location.href = '/manager/home';
  return false;
};
