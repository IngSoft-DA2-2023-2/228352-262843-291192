import { CanActivateFn } from "@angular/router";

export const authGuard: CanActivateFn = (route, state) => {
  const isAuthenticated = !!localStorage.getItem('sessionToken');

  if (!isAuthenticated) {
    window.location.href = '/login';
    return false;
  }
  return true;
};
