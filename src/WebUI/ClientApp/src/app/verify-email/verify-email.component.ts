import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  AccountClient,
  LoginDTO,
  RegisterCommand,
  RegisterDTO,
  UpdatestatusCommand,
} from '../SoftMash-Api';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../ApiAuthorization/AuthorizeService';
import { NavbarServiceService } from '../navbar-service.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.scss'],
})
export class VerifyEmailComponent implements OnInit {
  data: RegisterDTO | any = {
    id: 0,
    isVerified: Boolean,
  };
  currentUser: LoginDTO | any;
  constructor(
    private router: Router,
    private empClient: AccountClient,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private navbar: NavbarServiceService,
    private toastr: ToastrService
  ) {
    this.navbar.navbarVisible = false;
  }

  ngOnInit(): void {
    debugger;

    this.route.params.subscribe((params) => {
      this.data.id = params['id'];
    });
  }
  onApprove(): void {
    debugger;
    this.empClient
      .updateUserVerificationStatus(<UpdatestatusCommand>{
        id: this.data.id,
      })
      .subscribe((response) => {
        // this.approved();
        this.router.navigateByUrl('/home');
        console.log(response);
      }),
      (error: any) => {
        console.log(error);
      };

    // Logic for approve button click
  }

  onCancel() {
    this.router.navigateByUrl('/login');
    // Logic for cancel button click
  }
  approved(): void {
    this.toastr.success('You Are successfully Approved');
  }
}
