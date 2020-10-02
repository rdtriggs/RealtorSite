import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import {FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LocationModel } from 'src/app/model/location';
import { LocationService } from 'src/app/services/location.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.scss']
})
export class AddPropertyComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private zone: NgZone,
    private LocationService: LocationService,
    private alertify: AlertifyService) { }
    private file: File | null = null;

    Location: LocationModel = new LocationModel();
    checked = false;
    imagePath: any;
    imgURL: any;

    form: FormGroup = new FormGroup ({
      mlsNumber: new FormControl('', Validators.required),
      street1: new FormControl('', Validators.required),
      street2: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
      zipCode: new FormControl('', Validators.required),
      neighborhood: new FormControl(''),
      salesPrice: new FormControl('', Validators.required),
      dataListed: new FormControl(''),
      bedrooms: new FormControl(0, Validators.required),
      bathrooms: new FormControl(0, Validators.required),
      garageSize: new FormControl(0),
      squareFeet: new FormControl(0, Validators.required),
      lotSize: new FormControl(0),
      description: new FormControl(''),
      photoUrl: new FormControl(null, Validators.required),
      isActive: new FormControl(false, Validators.required),
    })

    initializeFormGroup() {
      this.form.setValue({
        mlsNumber: '',
        street1: '',
        street2: '',
        city: '',
        state: '',
        zipCode: '',
        neighborhood: '',
        salesPrice: 0,
        bedrooms: 0,
        bathrooms: 0,
        garageSize: 0,
        squareFeet: 0,
        lotSize: 0,
        description: '',
        isActive: false
      });
    }

    ngOnInit(): void {
    }

    getLocations() {
      return this.LocationService.getAllLocations();
    }

    onSubmit() {
      this.Location = this.form.value;
      this.Location.PhotoUrl = this.imgURL;
      this.zone.run(() => {
          this.LocationService.addLocation(this.Location).toPromise().then(res => this.router.navigate(['/buy-property']))
        })
      }

      onClear() {
        this.form.reset();
        this.initializeFormGroup();
      }

      onUpload(event) {
        var files = event.srcElement.files;
        this.file = files.item(0);
        this.addPhoto(files);
      }
      private addPhoto(files: any) {
        var reader = new FileReader();
        this.imagePath = files;
        reader.readAsDataURL(files[0]);
        reader.onload = (_event) => {
          this.imgURL = reader.result;
        };
      }
}
