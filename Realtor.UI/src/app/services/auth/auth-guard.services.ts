import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
              private authService: AuthService) { }

  canActivate() {
    if  (this.authService.isLoggedIn() ) {
      return true;
    }

    //TODO: REPLACE
    this.router.navigate(['/']);
    return false;
  }

}