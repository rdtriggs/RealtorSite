import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/services/auth/user.service';
import { AlertifyService } from 'src/app/services/alertify.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent implements OnInit {

  registerationForm: FormGroup;
  userSubmitted: boolean;
  constructor(private fb: FormBuilder,
              private userService: UserService,
              private alertify: AlertifyService,
              private router: Router ) { }

  ngOnInit() {
    this.createRegisterationForm();
  }

  createRegisterationForm() {
    this.registerationForm =  this.fb.group({
      userName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      confirmPassword: [null, Validators.required]
    }, {validators: this.passwordMatchingValidatior});
  }

  passwordMatchingValidatior(fg: FormGroup): Validators {
    return fg.get('password').value === fg.get('confirmPassword').value ? null :
    {notmatched: true};
  }

  onSubmit() {
    this.userService.addUser(this.registerationForm.value)
    .catch(function(error) {
      let alert = new AlertifyService();
      alert.error(error.message);
    })
    .then(res => this.router.navigate(['/buy-property']));
}

  onReset() {
    this.userSubmitted = false;
    this.registerationForm.reset();
    this.router.navigate(['/']);
  }


  //get form data for validation
  get userName() {
    return this.registerationForm.get('userName') as FormControl;
  }
  get email() {
    return this.registerationForm.get('email') as FormControl;
  }
  get password() {
    return this.registerationForm.get('password') as FormControl;
  }
  get confirmPassword() {
    return this.registerationForm.get('confirmPassword') as FormControl;
  }
}
