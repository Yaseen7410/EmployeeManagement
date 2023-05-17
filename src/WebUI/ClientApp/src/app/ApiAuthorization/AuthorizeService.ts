import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountClient, LoginCommand, LoginDTO } from '../SoftMash-Api';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<LoginDTO> | any;
  public currentUser: Observable<LoginDTO>;
  private isAuthenticated = false;

  constructor(
    private http: HttpClient,
    private empClient: AccountClient,
    private toastr: ToastrService,
    private jwt: JwtHelperService
  ) {
    this.currentUserSubject = new BehaviorSubject<LoginDTO>(
      JSON.parse(localStorage.getItem('currentUser')!)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }
  public get currentUserValue(): LoginDTO {
    return this.currentUserSubject.value;
  }
  login(email: string, password: string) {
    return this.empClient
      .loginRequest(<LoginCommand>{
        email,
        password,
      })
      .pipe(
        map((user) => {
          debugger;
          console.log(user);
          // login successful if there's a jwt token in the response
          if (user.succeeded == true) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserSubject.next(user);
          }

          return user;
        })
      );
  }
  logout() {
    // remove user from local storage to log user out
    this.logouttoast();
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
  logouttoast(): void {
    this.toastr.error('User Logedout Successfully');
  }
  isAuthenticatedUser(): boolean {
    debugger;
    if (this.currentUser) {
      return false;
    } else {
      return true;
    }
  }
}
