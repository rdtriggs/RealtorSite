import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.scss']
})

export class PropertyDetailComponent {
  constructor(private route: ActivatedRoute,
              private router: Router) { }
  }
