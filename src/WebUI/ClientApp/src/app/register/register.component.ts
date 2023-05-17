import { GridQuery, RolesDTO } from './../SoftMash-Api';
import { Component, OnInit } from '@angular/core';
import {
  AccountClient,
  RegisterCommand,
  RegisterDTO,
  RolesClient,
} from '../SoftMash-Api';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  private formBuilder: FormBuilder | any;
  loginForm: FormGroup | any;
  submitted = false;
  registrationForm: FormGroup | any;
  hidePassword: boolean = true;
  constructor(
    private empClient: AccountClient,
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private role: RolesClient
  ) {}

  errorMessage: string | any;
  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(10),
          Validators.pattern(
            /^(?=.*[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?])(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).*$/
          ),
        ],
      ],
      confirmPassword: ['', Validators.required],
      phoneNo: [
        '',
        Validators.required,
        // Validators.pattern('^[0-9]*$'),
        // Validators.maxLength(10),
      ],
      address: ['', Validators.required],
      rolesId: '',
    });
    this.getRoles();
  }
  succ: string | any;
  Register(): void {
    debugger;
    this.empClient
      .registerRequest(<RegisterCommand>{
        name: this.registrationForm.name,
        address: this.registrationForm.address,
        email: this.registrationForm.email,
        phoneNo: this.registrationForm.phoneNo,
        password: this.registrationForm.password,
        confirmPassword: this.registrationForm.confirmPassword,
        rolesId: this.registrationForm.rolesId,
      })
      .subscribe(
        (response) => {
          if (response.succeeded) {
            this.success();
            this.router.navigateByUrl('/login');
          } else {
            //console.log(response.errors);
            this.errorMessage = response.errors;
            this.fail();
          }
          // console.log(this.registerData.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }
  rolesData: any = [];
  query: GridQuery = <GridQuery>{
    page: 1,
    pageSize: 10,
    filter: {},
    ascending: true,
    sort: 'CreatedOn',
  };

  getRoles(): void {
    this.role.rolesQuery(this.query).subscribe((response) => {
      if (response.data) {
        this.rolesData = response.data;
      } else {
        this.rolesData = [];
      }
      console.log(this.rolesData);
    });
  }
  success(): void {
    this.toastr.success('User Registered Successfully');
  }
  fail(): void {
    this.toastr.warning(this.errorMessage, 'warning');
  }
  togglePasswordVisibility() {
    this.hidePassword = !this.hidePassword;
  }
  validatePhoneNumber(event: KeyboardEvent) {
    const input = event.key;
    const isDigit = /\d/.test(input);
    if (!isDigit) {
      event.preventDefault();
    }
  }
  onEmailInput(event: any) {
    const email = event.target.value;
    const dotComIndex = email.indexOf('.com');
    if (dotComIndex !== -1 && email.length > dotComIndex + 4) {
      event.target.value = email.slice(0, dotComIndex + 4);
      this.registrationForm.controls.email.setValue(event.target.value);
    }
  }
}
