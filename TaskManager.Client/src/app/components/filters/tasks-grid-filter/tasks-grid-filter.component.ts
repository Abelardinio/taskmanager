import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Utils } from '../../common/utils';
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
  @Output() public refresh = new EventEmitter<any>();

  public priorityArray = Utils.generateArray(100);

  public ngOnInit() {
    this.priorityArray.shift();
  }

  public onRefreshButtonClick() {
    if (!this.isRefreshing) {
      this.refresh.emit(this);
    }
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
    this.refresh.emit(this);
  }

  public get labels() { return Labels.Tasks; }
}
