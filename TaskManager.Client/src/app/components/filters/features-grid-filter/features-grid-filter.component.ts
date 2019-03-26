import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectFilter } from 'src/app/models/ProjectFilter';
import { Labels } from 'src/app/resources/labels';
import { FeatureFilter } from 'src/app/models/FeatureFilter';

@Component({
  selector: 'app-features-grid-filter',
  templateUrl: './features-grid-filter.component.html',
  styleUrls: ['./features-grid-filter.component.css']
})
export class FeaturesGridFilterComponent implements OnInit {

  @Input() public isRefreshing: boolean;
  @Input() public filter: FeatureFilter;
  @Output() public filterChange = new EventEmitter<FeatureFilter>();

  public ngOnInit() { }

  public onClearButtonClick() {
    this.filter.Value = null;
    this.onFilterChange();
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
  }

  public get labels() { return Labels.Features; }
}
