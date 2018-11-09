import { Component, OnInit } from '@angular/core';
import { Task } from '../../../models/Task';
import { TaskService } from '../../../services/TaskService';
import { finalize } from 'rxjs/operators';
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

@Component({
  selector: 'app-tasks-page',
  templateUrl: './tasks-page.component.html',
  styleUrls: ['./tasks-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class TasksPageComponent extends TableBase<Task, TaskSortingColumn> implements OnInit {
  public selectedTask = {};
  public pagesCount = 10;

  public headers: TableHeaderInfo<TaskSortingColumn>[] =
    [new TableHeaderInfo('Name', 'column', TaskSortingColumn.Name),
      new TableHeaderInfo('Priority', 'priority-column', TaskSortingColumn.Priority),
      new TableHeaderInfo('Added', 'added-column', TaskSortingColumn.Added),
      new TableHeaderInfo('Time to complete', 'time-to-complete-column', TaskSortingColumn.TimeToComplete),
      new TableHeaderInfo('Action', 'action-column', null, false)];

  public filter: TaskFilter = new TaskFilter(
                              new SortingInfo<TaskSortingColumn>(SortingOrder.Desc, TaskSortingColumn.Name),
                              new PagingInfo(1, 20));

  constructor(
    private _taskService: TaskService) {
    super();
  }

  public getAction(filter: TaskFilter): Observable<PagedResult<Task>> {
    return this._taskService.Get(filter);
  }

  public ngOnInit() {

    this._taskService.onTaskDeleted(id => super.removeRow(id));

    this._taskService.onTaskCompleted(id => { super.getRow(id).Status = TaskStatus.Completed; });

    super.ngOnInit();
  }

  public onCompleteButtonClick(element) {
    if (element.IsLoading) { return; }
    element.IsLoading = true;

    this._taskService.Complete(element.Id)
      .pipe(finalize(() => { element.IsLoading = false; }))
      .subscribe(
        () => { element.Status = TaskStatus.Completed; });
  }

  public onRemoveButtonClick(element) {
    if (element.IsLoading || element.Status === TaskStatus.Removed) { return; }
    element.IsLoading = true;

    this._taskService.Delete(element.Id)
      .pipe(finalize(() => { element.IsLoading = false; }))
      .subscribe(
        () => {
          element.Status = TaskStatus.Removed;
          super.removeRow(element.Id);
        });
  }

  public onRowSelected(task: Task) {
    if (task) {
      this.selectedTask = task;
    }
  }

  get taskStatus() { return TaskStatus; }
}
