// import { Injectable } from '@angular/core';
// import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
// import { Location } from 'src/app/model/location';
// import { Observable, of } from 'rxjs';
// import { LocationService } from 'src/app/services/location.service';
// import { catchError, map } from 'rxjs/operators';

// @Injectable({
//   providedIn: 'root'
// })
// export class PropertyDetailResolverService implements Resolve<Location> {

// constructor(private router: Router,  private LocationService: LocationService) { }

//TODO: RT - Didn't get around to properly adding this in this iteration.
// resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
//   Observable<Location>|Location {
//     const propId = route.params['id'];

//     
//     return this.LocationService.getProperty(propId).pipe(
//       catchError(error => {
//         this.router.navigate(['/']);
//         return of(null);
//       })
//     );
// }
// }
