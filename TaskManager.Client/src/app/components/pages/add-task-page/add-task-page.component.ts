import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Utils } from '../../../common/utils';
import { NotificationsService } from 'angular2-notifications';
import { TaskService } from '../../../services/TaskService';
import { TimeSpan, TaskInfo } from '../../../models/TaskInfo';
import { CustomValidators } from '../../common/custom-validators';
import { Observable } from 'rxjs';
import { Messages } from '../../../resources/messages';
import { Labels } from '../../../resources/labels';
import { FormBase } from '../../common/form-base/form-base';

@Component({
  selector: 'app-add-task-page',
  templateUrl: './add-task-page.component.html',
  styleUrls: ['./add-task-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class AddTaskPageComponent extends FormBase<TaskInfo> implements OnInit {
  public form: FormGroup;

  public priorityArray = Utils.generateArray(100);

  public constructor(
    protected notifications: NotificationsService,
    private _formBuilder: FormBuilder,
    private _taskService: TaskService) {
    super();
  }

  public ngOnInit() {
    this.form = this._formBuilder.group({
      name: new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(100)]),
      priority: new FormControl(1, [Validators.required, Validators.min(1), Validators.max(1000)]),
      description: new FormControl(''),
      timeToComplete: new FormControl(new TimeSpan(0, 0, 0), [CustomValidators.timepickerRequired])
    });

    this.priorityArray.shift();
  }

  protected submitAction(value: TaskInfo): Observable<Object> {
    return this._taskService.Add(value);
  }

  public get name() { return this.form.get('name'); }
  public get timeToComplete() { return this.form.get('timeToComplete'); }
  public get messages() { return Messages; }
  public get labels() { return Labels.Tasks; }

}
