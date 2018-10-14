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

  @Input() isRefreshing: boolean;
  @Input() filter: TaskFilter;
  @Output() filterChange = new EventEmitter<TaskFilter>();
  @Output() refresh = new EventEmitter<any>();
  constructor() { }

  public priorityArray = Utils.generateArray(100);

  ngOnInit() {
    this.priorityArray.shift();
  }

  onRefreshButtonClick() {
    if (!this.isRefreshing) {
      this.refresh.emit(this);
    }
  }

  onFilterChange() {
    this.filterChange.emit(this.filter);
    this.refresh.emit(this);
  }

  public get labels() { return Labels.Tasks; }
}
