import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';

import { AppComponent } from './app.component';
import { ErrorComponent } from './components/error/error.component';
import { AddPropertyComponent } from './components/property/add-property/add-property.component';
import { PropertyListComponent } from './components/property/property-list/property-list.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { LocationService } from './services/location.service';
import { UserRegisterComponent } from './components/user/user-register/user-register.component';
import { UserLoginComponent } from './components/user/user-login/user-login.component';
import { UserService } from './services/auth/user.service';
import { AlertifyService } from './services/alertify.service';
import { AuthService } from './services/auth/auth.service';
import { AuthGuard } from './services/auth/auth-guard.services';

import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { environment } from 'src/environments/environment';

const appRoutes: Routes = [
   {path: '', component: UserLoginComponent},
   {path: 'buy-property', canActivate: [AuthGuard], component: PropertyListComponent},
   {path: 'add-property', canActivate: [AuthGuard], component: AddPropertyComponent},
   // {path: 'property-detail/:id',
   //          canActivate: [AuthGuard],
   //         component: PropertyDetailComponent,
   //         resolve: {prp: PropertyDetailResolverService}},
   {path: 'user/login', component: UserLoginComponent},
   {path: 'user/register', component: UserRegisterComponent},
   { path: '404', component: ErrorComponent, pathMatch: 'full' },
   {path: '**', redirectTo: '404'}
 ]

@NgModule({
   declarations: [
      AppComponent,
      PropertyListComponent,
      AddPropertyComponent,
      NavBarComponent,
      UserRegisterComponent,
      UserLoginComponent,
   ],
   imports: [
      FormsModule,
      ReactiveFormsModule,
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(appRoutes),
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      ButtonsModule.forRoot(),
      BsDatepickerModule.forRoot(),
      NgxGalleryModule,
      AngularFireModule.initializeApp(environment.firebaseConfig),
      AngularFireDatabaseModule,
      AngularFireAuthModule,
      MatCardModule,
      MatTableModule,
      MatButtonModule,
      MatIconModule,
      MatFormFieldModule,
      MatInputModule,
      MatToolbarModule,
      MatGridListModule,
      MatCheckboxModule,
      MatDatepickerModule,
   ],
   providers: [
     LocationService,
     UserService,
     AlertifyService,
     AuthGuard,
     AuthService,
   ],
   bootstrap: [
      AppComponent
   ],
   schemas: [
      CUSTOM_ELEMENTS_SCHEMA,
      NO_ERRORS_SCHEMA
    ]
})
export class AppModule { }
