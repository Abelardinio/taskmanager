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

@Component({
  selector: 'app-tasks-page',
  templateUrl: './tasks-page.component.html',
  styleUrls: ['./tasks-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class TasksPageComponent implements OnInit {
  tasks: Task[] = [];
  selectedTask = {};
  isGridRefreshing: Boolean = false;
  pagesCount = 10;
  headers: TableHeaderInfo[] = [new TableHeaderInfo('Name', 'column', TaskSortingColumn.Name),
                                new TableHeaderInfo('Priority', 'priority-column', TaskSortingColumn.Priority),
                                new TableHeaderInfo('Added', 'added-column', TaskSortingColumn.Added),
                                new TableHeaderInfo('Time to complete', 'time-to-complete-column', TaskSortingColumn.TimeToComplete),
                                new TableHeaderInfo('Action', 'action-column', null, false)];
  filter: TaskFilter = new TaskFilter(SortingOrder.Desc, TaskSortingColumn.Name, new PagingInfo(1, 20));

  constructor(
    private _taskService: TaskService) {

  }

  ngOnInit() {
    this._fetchData();
  }

  onCompleteButtonClick(element) {
    if (element.IsLoading) { return; }
    element.IsLoading = true;

    this._taskService.Complete(element.Id)
      .pipe(finalize(() => { element.IsLoading = false; }))
      .subscribe(
        () => { element.Status = TaskStatus.Completed; });
  }

  onRemoveButtonClick(element) {
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

  onRowSelected(task: Task) {
    this._selectTask(task);
  }

  onRefresh() {
    this._fetchData();
  }

  _fetchData() {
    this.isGridRefreshing = true;
    this._taskService.Get(this.filter)
      .pipe(finalize(() => { this.isGridRefreshing = false; }))
      .subscribe(
        data => {
          this.tasks = [];
          this.pagesCount = (<any> data).PagesCount;
          this.tasks.push(...(<Task[]>(<any>data).Items));
        });
  }

  _selectTask(task: Task) {
    if (task) {
      this.selectedTask = task;
    } else {
      this.selectedTask = {};
    }
  }

  get taskStatus() { return TaskStatus; }
}
