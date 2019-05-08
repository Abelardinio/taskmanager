import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/Task';
import { UserTasksService } from 'src/app/services/UserTasksService';
import { Labels } from 'src/app/resources/labels';
import { TaskStatus } from 'src/app/models/enums/TaskStatus';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { TaskService } from 'src/app/services/TaskService';
import _ = require('lodash');

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  host: { 'class': 'flex-column flexible' },
  animations: [
    trigger('valueUpdated', [
      state('void => *', style({ opacity: 1, backgroundColor: 'white' })),
      transition('void => *', []),
      transition('* => *', [
        animate(500, style({ opacity: 0.3, backgroundColor: 'green' }))
      ])
    ])
  ]
})
export class HomePageComponent implements OnInit {

  public tasks: Task[];
  constructor(
    private _userTasksService: UserTasksService,
    private _taskService: TaskService) { }

  public ngOnInit() {
    this._userTasksService.Get().subscribe(result => this.tasks = result);
    this._taskService.onTaskDeleted(id => this._removeTask(id));
    this._taskService.onTaskCompleted(id => this._completeTask(id));
    this._taskService.onTaskUnassigned(id => this._removeTask(id));
    this._taskService.onTaskAssigned(task => this._addTask(task));
  }

  private _removeTask(id: number) {
    _.remove(this.tasks, task => task.Id === id);
  }

  private _addTask(task: Task) {
    if (!this.tasks.find(x => x.Id === task.Id)) {
      this.tasks.push(task);
    }
  }

  private _completeTask(id: number) {
    const task = this.tasks.find(x => x.Id === id);

    if (task) {
      task.Status = TaskStatus.Completed;
    }
  }

  public get labels() { return Labels.Tasks; }
  public taskStatus(status: TaskStatus) { return TaskStatus[status]; }
}
