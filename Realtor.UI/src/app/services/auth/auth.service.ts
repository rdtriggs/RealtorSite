import { Injectable, NgZone } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import * as firebase from 'firebase/app';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
private user: Observable<firebase.User>;
private userDetails: firebase.User = null;

constructor(private _firebaseAuth: AngularFireAuth, private router: Router, private zone: NgZone) 
  { 
  this.user = _firebaseAuth.authState;
  this.user.subscribe(
    (user) => {
      if (user) {
        this.userDetails = user;
      } else {
        this.userDetails = null;
      }
    }
  );
}

  logout() {
    localStorage.setItem("IsLoggedIn", "false");
    this._firebaseAuth.signOut()
    .then((res) => this.zone.run(() => this.router.navigate(['/'])));
  }

  isLoggedIn() {
    let user = firebase.auth().currentUser;
    return user ? true : false;
    }

  signInWithEmail(user: any) {
    return this._firebaseAuth.signInWithEmailAndPassword(user.email, user.password).then(res=> localStorage.setItem("IsLoggedIn", "true"))
  }

  signInWithFacebook() {
    return this._firebaseAuth.signInWithPopup(
      new firebase.auth.FacebookAuthProvider()
    )
  }

  signInWithGithub() {
    return this._firebaseAuth.signInWithPopup(
      new firebase.auth.GithubAuthProvider()
    )
  }

  signInWithGoogle() {
    return this._firebaseAuth.signInWithPopup(
      new firebase.auth.GoogleAuthProvider())
  }

  signInWithTwitter() {
    return this._firebaseAuth.signInWithPopup(
      new firebase.auth.TwitterAuthProvider()
    )
  }
}
