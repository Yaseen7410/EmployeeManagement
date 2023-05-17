import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NavbarServiceService {
  private hideNavbar: boolean = true;
  get navbarVisible(): boolean {
    return this.hideNavbar;
  }
  set navbarVisible(value: boolean) {
    this.hideNavbar = value;
  }
}
