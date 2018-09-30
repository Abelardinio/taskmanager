import { Component, OnInit } from '@angular/core';
import { Task } from '../../../models/Task';
import { TaskService } from '../../../services/TaskService';
import { finalize } from 'rxjs/operators';
import { TaskStatus } from '../../../models/enums/TaskStatus';

@Component({
  selector: 'app-tasks-page',
  templateUrl: './tasks-page.component.html',
  styleUrls: ['./tasks-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class TasksPageComponent implements OnInit {
  tasks: Task[] = [];
  selectedTask = {};

  constructor(
    private _taskService: TaskService) {

  }

  ngOnInit() {
    this._taskService.Get().subscribe(
      data => {
        this.tasks.push(...(<Task[]>data));
      });
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


  _selectTask(task: Task) {
    if (task) {
      this.selectedTask = task;
    } else {
      this.selectedTask = {};
    }
  }

  get taskStatus() { return TaskStatus; }
}
