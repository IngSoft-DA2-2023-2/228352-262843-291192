// auth-redirect.guard.ts
import { CanActivateFn } from '@angular/router';

export const authRedirectGuard: CanActivateFn = (route, state) => {
  const isAuthenticated = !!localStorage.getItem('sessionToken');

  if (isAuthenticated) {
    window.location.href = '/home';
    return false;
  }
  return true;
};