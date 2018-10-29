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

@Component({
  selector: 'app-tasks-page',
  templateUrl: './tasks-page.component.html',
  styleUrls: ['./tasks-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class TasksPageComponent implements OnInit {
  public tasks: Task[] = [];
  public selectedTask = {};
  public isGridRefreshing: Boolean = false;
  public pagesCount = 10;
  public headers: TableHeaderInfo<TaskSortingColumn>[] = [new TableHeaderInfo('Name', 'column', TaskSortingColumn.Name),
                                       new TableHeaderInfo('Priority', 'priority-column', TaskSortingColumn.Priority),
                                       new TableHeaderInfo('Added', 'added-column', TaskSortingColumn.Added),
                                       new TableHeaderInfo('Time to complete', 'time-to-complete-column', TaskSortingColumn.TimeToComplete),
                                       new TableHeaderInfo('Action', 'action-column', null, false)];
  public filter: TaskFilter = new TaskFilter(
                              new SortingInfo<TaskSortingColumn>(SortingOrder.Desc, TaskSortingColumn.Name),
                              new PagingInfo(1, 20));

  constructor(
    private _taskService: TaskService) {

  }

  public ngOnInit() {
    this._fetchData();
    this._taskService.onTaskDeleted((id) => { console.log(id); });
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
    if (element.IsLoading) { return; }
    element.IsLoading = true;

    this._taskService.Delete(element.Id)
      .pipe(finalize(() => { element.IsLoading = false; }))
      .subscribe(
        () => {
          element.Status = TaskStatus.Removed;
          this.tasks.splice(this.tasks.indexOf(element), 1);
        });
  }

  public onRowSelected(task: Task) {
    this._selectTask(task);
  }

  public onRefresh() {
    this._fetchData();
  }

  private _fetchData() {
    this.isGridRefreshing = true;
    this._taskService.Get(this.filter)
      .pipe(finalize(() => { this.isGridRefreshing = false; }))
      .subscribe(
        data => {
          this.tasks = [];
          this.pagesCount = data.PagesCount;
          this.tasks.push(...data.Items);
        });
  }

  private _selectTask(task: Task) {
    if (task) {
      this.selectedTask = task;
    } else {
      this.selectedTask = {};
    }
  }

  get taskStatus() { return TaskStatus; }
}
