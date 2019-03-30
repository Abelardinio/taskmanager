import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Labels } from 'src/app/resources/labels';
import { FeatureFilter } from 'src/app/models/FeatureFilter';
import { Lookup } from 'src/app/models/Lookup';
import { ProjectService } from 'src/app/services/ProjectService';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-features-grid-filter',
  templateUrl: './features-grid-filter.component.html',
  styleUrls: ['./features-grid-filter.component.css']
})
export class FeaturesGridFilterComponent implements OnInit {

  @Input() public isRefreshing: boolean;
  @Input() public filter: FeatureFilter;
  @Output() public filterChange = new EventEmitter<FeatureFilter>();

  public constructor(private _projectService: ProjectService) {
  }

  public projectsArray: Lookup[];

  public ngOnInit() {
    this._projectService.GetLookup().subscribe(projects => this.projectsArray = projects);
   }

  public onClearButtonClick() {
    this.filter.Value = null;
    this.filter.ProjectId = null;
    this.onFilterChange();
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
  }

  public get labels() { return Labels.Features; }
}
