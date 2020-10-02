import { Injectable } from '@angular/core';
import * as firebase from 'firebase/app';

@Injectable({
  providedIn: 'root'
})
export class UserService {

constructor() { }

addUser(user: any) {
    return firebase.auth().createUserWithEmailAndPassword(user.email, user.password);
  }
}
