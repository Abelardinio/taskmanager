import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Utils } from '../../common/utils';

@Component({
  selector: 'app-tasks-grid-filter',
  templateUrl: './tasks-grid-filter.component.html',
  styleUrls: ['./tasks-grid-filter.component.css']
})
export class TasksGridFilterComponent implements OnInit {

  @Input() isRefreshing: boolean;
  @Output() refreshButtonClick = new EventEmitter<any>();
  constructor() { }

  public priorityArray = Utils.generateArray(100);

  ngOnInit() {
  }

  onRefreshButtonClick() {
    if (!this.isRefreshing) {
      this.refreshButtonClick.emit(this);
    }
  }
}
