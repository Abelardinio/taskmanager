import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { Observable } from 'rxjs';
import { Messages } from '../../../resources/messages';
import { Labels } from '../../../resources/labels';
import { FormBase } from '../../common/form-base/form-base';
import { UserInfo } from 'src/app/models/UserInfo';
import { UserService } from 'src/app/services/UserService';
import { Lookup } from 'src/app/models/Lookup';
import { Lookups } from 'src/app/resources/Lookups';
import { Role } from 'src/app/models/enums/Role';
@Component({
    selector: 'app-add-user-page',
    templateUrl: './add-user-page.component.html',
    host: { 'class': 'flex-column flexible' },
    styleUrls: ['./add-user-page.component.css']
  })

  export class AddUserPageComponent extends FormBase<UserInfo> implements OnInit {
    public form: FormGroup;
    public rolesArray: Lookup[] = Lookups.Roles;

    public constructor(
      protected notifications: NotificationsService,
      private _formBuilder: FormBuilder,
      private _userService: UserService) {
      super();
    }

    public ngOnInit() {
      this.form = this._formBuilder.group({
        username: new FormControl('', [Validators.required, Validators.maxLength(this.maxLength)]),
        firstName: new FormControl('', [Validators.required, Validators.maxLength(this.maxLength)]),
        lastName: new FormControl('', [Validators.required, Validators.maxLength(this.maxLength)]),
        email: new FormControl('', [Validators.email, Validators.required, Validators.maxLength(this.maxLength)]),
        role: new FormControl(Role.User)
      });
    }

    protected submitAction(value: UserInfo): Observable<Object> {
      return this._userService.Add(value);
    }

    public get username() { return this.form.get('username'); }
    public get firstName() { return this.form.get('firstName'); }
    public get lastName() { return this.form.get('lastName'); }
    public get email() { return this.form.get('email'); }
    public get messages() { return Messages; }
    public get labels() { return Labels.Users; }
    private get maxLength() { return 100; }
  }
