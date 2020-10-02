import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../services/alertify.service';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  loggedinUser: string;
  constructor(private alertify: AlertifyService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  loggedin() {
    if (localStorage.getItem("IsLoggedIn") === "true")
    {
      return true;
    }
    else
    return false;
  }

  onLogout() {
    this.alertify.success("You are logged out !");
    this.authService.logout();
  }

}
