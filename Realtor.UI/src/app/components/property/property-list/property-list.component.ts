import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { LocationService } from '../../../services/location.service';
import { ActivatedRoute } from '@angular/router';
import { LocationModel } from '../../../model/location';
import { AlertifyService } from '../../../services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.scss']
})
export class PropertyListComponent implements OnInit {
  LocationList: Array<LocationModel> = new Array<LocationModel>();
  displayedColumns: string[] = ['street1', 'street2', 'city', 'state', 'zipCode', 'salesPrice', 'neighborhood', 'delete'];
 
  constructor(private route: ActivatedRoute, 
    private LocationService: LocationService, 
    private zone: NgZone, 
    private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.zone.run(() => {
      this.getLocations().toPromise().then((data) => {
        this.LocationList = data || [];
      })
    })
  }

  getLocations() {
    return this.LocationService.getAllLocations();
  }

  delete(element){
    return this.LocationService.deleteLocation(element);
  }

  deleteLocation(element){
    this.zone.run(() => { 
      this.delete(element).toPromise().then(res => this.reloadComponent())
      this.alertify.success("Property Listing Deleted.");
    })
  };

  //This allows for reloading the same route.
  reloadComponent() {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate(['/buy-property']);
}
}
