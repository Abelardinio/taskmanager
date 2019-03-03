import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserFilter } from 'src/app/models/UserFilter';
import { Labels } from 'src/app/resources/labels';

@Component({
  selector: 'app-users-grid-filter',
  templateUrl: './users-grid-filter.component.html',
  styleUrls: ['./users-grid-filter.component.css']
})
export class UsersGridFilterComponent implements OnInit {

  @Input() public isRefreshing: boolean;
  @Input() public filter: UserFilter;
  @Output() public filterChange = new EventEmitter<UserFilter>();

  public ngOnInit() { }

  public onClearButtonClick() {
    this.filter.Value = null;
    this.onFilterChange();
  }

  public onFilterChange() {
    this.filterChange.emit(this.filter);
  }

  public get labels() { return Labels.Users; }
}
