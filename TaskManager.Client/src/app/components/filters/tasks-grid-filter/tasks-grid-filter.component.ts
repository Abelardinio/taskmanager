import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Utils } from '../../../common/utils';
import { Labels } from '../../../resources/labels';
import { TaskFilter } from '../../../models/TaskFilter';
import { ProjectService } from 'src/app/services/ProjectService';
import { FeatureService } from 'src/app/services/FeatureService';
import { Lookup } from 'src/app/models/Lookup';

@Component({
  selector: 'app-tasks-grid-filter',
  templateUrl: './tasks-grid-filter.component.html',
  styleUrls: ['./tasks-grid-filter.component.css']
})
export class TasksGridFilterComponent implements OnInit {

  @Input() public isRefreshing: boolean;
  @Input() public filter: TaskFilter;
  @Output() public filterChange = new EventEmitter<TaskFilter>();

  public priorityArray = Utils.generateArray(100);

  public constructor(
    private _projectService: ProjectService,
    private _featureService: FeatureService) {
  }

  public projectsArray: Lookup[];
  public featuresArray: Lookup[];

  public ngOnInit() {
    this.priorityArray.shift();
    this._projectService.GetLookup().subscribe(projects => this.projectsArray = projects);
    this._featureService.GetLookup().subscribe(features => this.featuresArray = features);
  }

  public onClearButtonClick() {
    this.filter.AddedFrom = null;
    this.filter.AddedTo = null;
    this.filter.Name = null;
    this.filter.Priority = null;

    if (this.filter.ProjectId) {
      this.filter.ProjectId = null;
      this.onProjectChange();
    }

    this.filter.ProjectId = null;
    this.filter.FeatureId = null;
    this.onFilterChange();
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
  }

  public onProjectChange() {
    this.filter.FeatureId = null;
    this._featureService.GetLookup(this.filter.ProjectId).subscribe(features => this.featuresArray = features);
    this.onFilterChange();
  }

  public get labels() { return Labels.Tasks; }
}
