import { Component, OnInit, Input } from '@angular/core';
import { TaskStatus } from '../../../models/enums/TaskStatus';
import { Labels } from '../../../resources/labels';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.css']
})
export class TaskDetailsComponent implements OnInit {

  @Input()
  task;

  constructor() { }

  ngOnInit() {
  }

  public get taskStatus() { return TaskStatus[this.task.Status]; }
  public get labels() { return Labels.Tasks}
}
