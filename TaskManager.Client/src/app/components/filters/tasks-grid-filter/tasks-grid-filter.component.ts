import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Utils } from '../../../common/utils';
import { Labels } from '../../../resources/labels';
import { TaskFilter } from '../../../models/TaskFilter';

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

  public ngOnInit() {
    this.priorityArray.shift();
  }

  public onClearButtonClick() {
    this.filter.AddedFrom = null;
    this.filter.AddedTo = null;
    this.filter.Name = null;
    this.filter.Priority = null;
    this.onFilterChange();
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
  }

  public get labels() { return Labels.Tasks; }
}
