import { Component, OnInit } from '@angular/core';
import { FormBase } from '../../common/form-base/form-base';
import { LoginModel } from 'src/app/models/LoginModel';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { LoginService } from 'src/app/services/LoginService';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { LocalStorageAccessor } from 'src/app/common/LocalStorageAccessor';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent extends FormBase<LoginModel> implements OnInit {
  public form: FormGroup;

  public constructor(
    protected notifications: NotificationsService,
    private _formBuilder: FormBuilder,
    private _loginService: LoginService,
    private _router: Router,
    private _localStorageAccessor: LocalStorageAccessor) {
    super();
  }

  protected submitAction(value: LoginModel): Observable<Object> {
    return this._loginService.Auth(value);
  }

  protected onSuccess(authResult) {
    this._localStorageAccessor.token = authResult.Token;
    this._router.navigate(['/']);
  }

  public ngOnInit() {
    this.form = this._formBuilder.group({
      username: new FormControl(''),
      password: new FormControl('')
    });
  }

  public get isSubmitDisabled() { return this.formIsLoading || !(this.form.get('username').value && this.form.get('password').value); }
}
