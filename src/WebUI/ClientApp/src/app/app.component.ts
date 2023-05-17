import { AuthenticationService } from './ApiAuthorization/AuthorizeService';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDTO } from './SoftMash-Api';
import { NavbarServiceService } from './navbar-service.service';
@Component({ selector: 'app-root', templateUrl: 'app.component.html' })
export class AppComponent {
  title = 'ClientApp';
  currentUser: LoginDTO | any;
  
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    public nav: NavbarServiceService
  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.currentUser = x)
    );
  }


  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
