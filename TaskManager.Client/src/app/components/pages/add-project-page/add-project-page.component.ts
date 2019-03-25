import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { Observable } from 'rxjs';
import { Messages } from '../../../resources/messages';
import { Labels } from '../../../resources/labels';
import { FormBase } from '../../common/form-base/form-base';
import { ProjectInfo } from 'src/app/models/ProjectInfo';
import { ProjectService } from 'src/app/services/ProjectService';
@Component({
    selector: 'app-add-project-page',
    templateUrl: './add-project-page.component.html',
    styleUrls: ['./add-project-page.component.css'],
    host: { 'class': 'flex-column flexible' }
  })

  export class AddProjectPageComponent extends FormBase<ProjectInfo> implements OnInit {
    public form: FormGroup;

    public constructor(
      protected notifications: NotificationsService,
      private _formBuilder: FormBuilder,
      private _projectService: ProjectService) {
      super();
    }

    public ngOnInit() {
      this.form = this._formBuilder.group({
        name: new FormControl('', [Validators.required, Validators.maxLength(this.maxLength)]),
        description: new FormControl('')
      });
    }

    protected submitAction(value: ProjectInfo): Observable<Object> {
      return this._projectService.Add(value);
    }

    public get name() { return this.form.get('name'); }
    public get description() { return this.form.get('description'); }
    public get messages() { return Messages; }
    public get labels() { return Labels.Projects; }
    private get maxLength() { return 100; }
  }
