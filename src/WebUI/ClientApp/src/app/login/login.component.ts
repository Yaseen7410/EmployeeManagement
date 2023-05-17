import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first, map } from 'rxjs/operators';
import { AuthenticationService } from '../ApiAuthorization/AuthorizeService';
import { ToastrService } from 'ngx-toastr';
import { Result } from '../SoftMash-Api';
@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  loginForm: FormGroup | any;
  loading = false;
  submitted = false;
  returnUrl: string = '';
  errorMessage: string | any;
  successMessage: string | any;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private toastr: ToastrService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/home']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    debugger;
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.authenticationService
      .login(this.f.email.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        (data: any) => {
          debugger;
          if (data.succeeded) {
            this.loading = false;
            this.success();
            // this.successMessage = data.lists;
            // console.log(this.successMessage);
            this.router.navigateByUrl('/home');
          } else {
            console.log(data.errors);
    
            this.errorMessage = data.errors;
           
            this.loading = false;
            this.router.navigateByUrl('/login');
          }

          //  this.errorMessage = localStorage.getItem('currentUser');

          //  splitString(this.errorMessage);
          // alert(this.errorMessage);
        },
        (error: any) => {
          this.router.navigateByUrl('/login');
          this.loading = false;
        }
      );
  }
  success(): void {
    this.toastr.success('User Login successfully');
  }
}

// function splitString(g: string) {
//   debugger;
//   const separator = ',';
//   const result = g.split(separator);
//   var error = result[1];
//   console.log(error);
// }
