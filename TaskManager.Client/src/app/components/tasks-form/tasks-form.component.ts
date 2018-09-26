import { Component, OnInit, OnDestroy } from '@angular/core';
import { TaskService } from '../../services/TaskService';
export { TaskStatus } from '../../models/enums/TaskStatus';
import { finalize } from 'rxjs/operators';
import { Task } from '../../models/Task';
import { TaskStatus } from '../../models/enums/TaskStatus';

@Component({
  selector: 'app-tasks-form',
  templateUrl: './tasks-form.component.html',
  styleUrls: ['./tasks-form.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class TasksFormComponent implements OnInit, OnDestroy {
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

  ngOnDestroy() {
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


