import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-tasks-grid-filter',
  templateUrl: './tasks-grid-filter.component.html',
  styleUrls: ['./tasks-grid-filter.component.css']
})
export class TasksGridFilterComponent implements OnInit {

  @Input() isRefreshing: boolean;
  @Output() refreshButtonClick = new EventEmitter<any>();
  constructor() { }


  ngOnInit() {
    this.refreshButtonClick
  }

  onRefreshButtonClick() {
    if (!this.isRefreshing){
      this.refreshButtonClick.emit(this);
    }
  }
}
