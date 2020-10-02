import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.logout();
  }

  onLogin(loginForm: NgForm) {
      this.authService.signInWithEmail(loginForm.value)
      .catch(function(error) {
        let alert = new AlertifyService();
        alert.error(error.message);
      })
      .then(res => this.router.navigate(['/buy-property']));
  }

  signInWithGoogle() {
    this.authService.signInWithGoogle()
    .catch(function(error) {
      let alert = new AlertifyService();
      alert.error(error.message);
    })
    .then(res => this.router.navigate(['/buy-property']));
  }

  signInWithFacebook() {
    this.authService.signInWithFacebook()
    .catch(function(error) {
      let alert = new AlertifyService();
      alert.error(error.message);
    })
    .then(res => this.router.navigate(['/buy-property']));
  }

  signInWithGithub() {
    this.authService.signInWithGithub()
    .catch(function(error) {
      let alert = new AlertifyService();
      alert.error(error.message);
    })
    .then(res => this.router.navigate(['/buy-property']));
  }

  signInWithTwitter() {
    this.authService.signInWithTwitter()
    .catch(function(error) {
      let alert = new AlertifyService();
      alert.error(error.message);
    })
    .then(res => this.router.navigate(['/buy-property']));
  }
}
