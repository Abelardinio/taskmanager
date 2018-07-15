import { Component, OnInit } from '@angular/core';
import { TaskInfo, TimeSpan } from '../../models/TaskInfo';
import { NotificationsService } from 'angular2-notifications';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { TaskService } from '../../services/TaskService';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-add-task-form',
  templateUrl: './add-task-form.component.html',
  styleUrls: ['./add-task-form.component.css'],
  host: { 'class': 'flex-column flexible' }
})

export class AddTaskFormComponent implements OnInit {

  public form: FormGroup;
  public daysArray = this._generateArray(7);
  public hoursArray = this._generateArray(24);
  public weeksArray = this._generateArray(52);
  public priorityArray = this._generateArray(100);
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
      days: new FormControl(0),
      hours: new FormControl(0),
      weeks: new FormControl(0)
    });

    this.priorityArray.shift();
  }

  public onSubmit() {

    if (this.form.valid && !this.timeToCompleteIsInvalid) {
      let result = this.form.value;
      let taskInfo = new TaskInfo(result.name, result.description, result.priority, new TimeSpan(result.weeks, result.days, result.hours));

      this.form.disable();
      this.formIsLoading = true;
      this._taskService.Add(taskInfo)
        .pipe(finalize(() => this._onSubmitEnd()))
        .subscribe(
          data => this._notifications.success('Task was successfuly added.'),
          error => this._notifications.error('Task was not added. Something went wrong.'));
    } else {
      this.formIsValidated = true;
      this._notifications.info('All the highlighted fields should be completed.');
    }
  }

  public get timeToCompleteIsInvalid() {
    var form = this.form;

    return form.get('weeks').value === 0 &&
      form.get('days').value === 0 &&
      form.get('hours').value === 0;
  }

  public get name() { return this.form.get('name'); }

  private _generateArray(n: number): Array<number> {
    return Array.apply(null, { length: n }).map(Function.call, Number);
  }

  private _onSubmitEnd() {
    this.formIsValidated = false;
    this.form.enable();
    this.formIsLoading = false;
  }
}
