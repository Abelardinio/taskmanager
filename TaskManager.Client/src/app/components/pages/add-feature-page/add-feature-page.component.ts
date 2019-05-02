import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { Observable } from 'rxjs';
import { Messages } from '../../../resources/messages';
import { Labels } from '../../../resources/labels';
import { FormBase } from '../../common/form-base/form-base';
import { FeatureInfo } from 'src/app/models/FeatureInfo';
import { FeatureService } from 'src/app/services/FeatureService';
import { ProjectService } from 'src/app/services/ProjectService';
import { Lookup } from 'src/app/models/Lookup';
@Component({
    selector: 'app-add-feature-page',
    templateUrl: './add-feature-page.component.html',
    styleUrls: ['./add-feature-page.component.css'],
    host: { 'class': 'flex-column flexible' }
  })

  export class AddFeaturePageComponent extends FormBase<FeatureInfo> implements OnInit {
    public form: FormGroup;
    public projectsArray: Lookup[];

    public constructor(
      protected notifications: NotificationsService,
      private _formBuilder: FormBuilder,
      private _featureService: FeatureService,
      private _projectService: ProjectService) {
      super();
    }

    public ngOnInit() {
      this.form = this._formBuilder.group({
        name: new FormControl('', [Validators.required, Validators.maxLength(this.maxLength)]),
        description: new FormControl(''),
        projectId: new FormControl('', [Validators.required])
      });

      this._projectService.GetLookupAddFeature().subscribe(projects => this.projectsArray = projects);
    }

    protected submitAction(value: FeatureInfo): Observable<Object> {
      return this._featureService.Add(value);
    }

    public get name() { return this.form.get('name'); }
    public get description() { return this.form.get('description'); }
    public get projectId() { return this.form.get('projectId'); }
    public get messages() { return Messages; }
    public get labels() { return Labels.Features; }
    private get maxLength() { return 100; }
  }
