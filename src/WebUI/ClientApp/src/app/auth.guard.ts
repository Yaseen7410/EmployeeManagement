import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
} from '@angular/router';
import { AuthenticationService } from './ApiAuthorization/AuthorizeService';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private logg: AuthenticationService, private router: Router) {}
  canActivate(route: ActivatedRouteSnapshot): boolean {
    debugger;
    const currentUser = this.logg.currentUserValue;
    if (currentUser) {
      return true;
    } else {
      alert('You are Not Authorized');
      this.router.navigate(['/login']);
      return false;
    }
  }
}
