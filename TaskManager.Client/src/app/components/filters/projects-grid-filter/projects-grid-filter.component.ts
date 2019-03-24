import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProjectFilter } from 'src/app/models/ProjectFilter';
import { Labels } from 'src/app/resources/labels';

@Component({
  selector: 'app-projects-grid-filter',
  templateUrl: './projects-grid-filter.component.html',
  styleUrls: ['./projects-grid-filter.component.css']
})
export class ProjectsGridFilterComponent implements OnInit {

  @Input() public isRefreshing: boolean;
  @Input() public filter: ProjectFilter;
  @Output() public filterChange = new EventEmitter<ProjectFilter>();

  public ngOnInit() { }

  public onClearButtonClick() {
    this.filter.Value = null;
    this.onFilterChange();
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
  }

  public get labels() { return Labels.Projects; }
}
