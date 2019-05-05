import { Component, OnInit, Input } from '@angular/core';
import { Task } from '../../../models/Task';
import { TaskService } from '../../../services/TaskService';
import { TaskStatus } from '../../../models/enums/TaskStatus';
import { TableHeaderInfo } from '../../common/table/table-header/TableHeaderInfo';
import { TaskSortingColumn } from '../../../models/enums/TaskSortingColumn';
import { TaskFilter } from '../../../models/TaskFilter';
import { SortingOrder } from '../../../models/enums/SortingOrder';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { SortingInfo } from 'src/app/models/SortingInfo';
import * as _ from 'lodash';
import { TableBase } from '../../common/table-base/table-base';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';
import { AuthService } from 'src/app/common/AuthService';


@Component({
  selector: 'app-tasks-table',
  templateUrl: './tasks-table.component.html',
  styleUrls: ['./tasks-table.component.css']
})
export class TasksTableComponent extends TableBase<Task> implements OnInit {
  @Input() public filteringIsEnabled = true;
  @Input() public featureIsSelected: boolean;

  public UserId: number;

  @Input() public set featureId(value: number) {
    if (value === undefined) {
      return;
    }

    this.filter.FeatureId = value;
    this.onRefresh();
  }

  public headers: TableHeaderInfo<TaskSortingColumn>[] =
    [new TableHeaderInfo('Name', 'column', TaskSortingColumn.Name),
    new TableHeaderInfo('Priority', 'priority-column', TaskSortingColumn.Priority),
    new TableHeaderInfo('Added', 'added-column', TaskSortingColumn.Added),
    new TableHeaderInfo('Time to complete', 'time-to-complete-column', TaskSortingColumn.TimeToComplete),
    new TableHeaderInfo('Assigned', 'assigned-column', null, false),
    new TableHeaderInfo('Action', 'action-column', null, false)];

  public filter: TaskFilter = new TaskFilter(
    new SortingInfo<TaskSortingColumn>(SortingOrder.Desc, TaskSortingColumn.Name),
    new PagingInfo(1, 20));

  constructor(
    private _taskService: TaskService,
    private _authService: AuthService) {
    super();
  }

  public getAction(filter: TaskFilter): Observable<PagedResult<Task>> {
    return this._taskService.Get(filter);
  }

  public ngOnInit() {
    this.fetchDataOnInit = !this.featureIsSelected;

    this._taskService.onTaskDeleted(id => super.removeRow(id));

    this._taskService.onTaskCompleted(id => super.updateRow(id, (row) => { row.Status = TaskStatus.Completed; }));

    this.UserId = this._authService.UserId;

    super.ngOnInit();
  }

  public onCompleteButtonClick(element: Task) {
    this.rowAjaxAction(
      element.Id,
      () => this._taskService.Complete(element.Id),
      () => super.updateRow(element.Id, (row) => { row.Status = TaskStatus.Completed; }));
  }

  public onRemoveButtonClick(element: Task) {
    this.rowAjaxAction(
      element.Id,
      () => this._taskService.Delete(element.Id),
      () => super.removeRow(element.Id));
  }

  public onAssignButtonClick(element: Task){
    this.rowAjaxAction(
      element.Id,
      () => this._taskService.Assign(element.Id),
      () => super.updateRow(element.Id, (row) => { row.AssignedUserId = this.UserId; }));
  }

  get taskStatus() { return TaskStatus; }
}
