import { Component, OnInit } from '@angular/core';
import { Feature } from 'src/app/models/Feature';
import { FeatureService } from 'src/app/services/FeatureService';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-feature-page',
  templateUrl: './feature-page.component.html',
  styleUrls: ['./feature-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class FeaturePageComponent implements OnInit {
  public feature: Feature;
  constructor(
    private _featureService: FeatureService,
    private _route: ActivatedRoute) { }

  public ngOnInit() {
    const featureId = Number(this._route.snapshot.paramMap.get('id'));
    this._featureService.GetById(featureId).subscribe(feature => this.feature = feature);
  }
}
