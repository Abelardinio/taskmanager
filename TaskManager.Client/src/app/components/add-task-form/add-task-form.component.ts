import { Component, OnInit } from '@angular/core';
import { TimeSpan } from '../../models/TaskInfo';
import { NotificationsService } from 'angular2-notifications';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { TaskService } from '../../services/TaskService';
import { finalize } from 'rxjs/operators';
import { CustomValidators } from '../common/custom-validators';
import { Utils } from '../common/utils';
import { Messages } from '../../resources/messages';
import { Labels } from '../../resources/labels';

@Component({
  selector: 'app-add-task-form',
  templateUrl: './add-task-form.component.html',
  styleUrls: ['./add-task-form.component.css'],
  host: { 'class': 'flex-column flexible' }
})

export class AddTaskFormComponent implements OnInit {

  public form: FormGroup;
  public priorityArray = Utils.generateArray(100);
  public formIsValidated = false;
  public formIsLoading = false;

  public constructor(
    private _notifications: NotificationsService,
    private _formBuilder: FormBuilder,
    private _taskService: TaskService) { }

  public ngOnInit() {
    this.form = this._formBuilder.group({
      name: new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(100)]),
      priority: new FormControl(1, [Validators.required, Validators.min(1), Validators.max(1000)]),
      description: new FormControl(''),
      timeToComplete: new FormControl(new TimeSpan(0,0,0), [CustomValidators.timepickerRequired])
    });

    this.priorityArray.shift();
  }

  public onSubmit() {

    if (this.form.valid) {
      let result = this.form.value;
            
      this.form.disable();
      this.formIsLoading = true;
      this._taskService.Add(result)
        .pipe(finalize(() => this._onSubmitEnd()))
        .subscribe();
    } else {
      this.formIsValidated = true;
      this._notifications.info(Messages.Common.FormValidationMessage);
    }
  }

  public get name() { return this.form.get('name'); }
  public get timeToComplete() { return this.form.get('timeToComplete'); }
  public get messages(){ return Messages.Tasks.Validation}
  public get labels(){return Labels.Tasks}

  private _onSubmitEnd() {
    this.formIsValidated = false;
    this.form.enable();
    this.formIsLoading = false;
  }
}
