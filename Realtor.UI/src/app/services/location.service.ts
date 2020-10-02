import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LocationModel } from '../model/location';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private apiUrl: string;

  constructor(private http: HttpClient) {}

  getAllLocations(): Observable<LocationModel[]> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    const options = { headers: headers }
    const url = environment.apiUrl + '/Location';
    return this.http.get<LocationModel[]>(url, options);
  }

  addLocation(param) {
    const headers = new HttpHeaders({
      Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('token'))
    });
    const options = { headers: headers }
    const url = environment.apiUrl + '/Location';
    return this.http.post(url, param, options);
  }

  deleteLocation(param) {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    const options = { headers: headers }
    const url = environment.apiUrl + '/Location/DeleteLocation';
    return this.http.post(url, param, options);
  }

}
