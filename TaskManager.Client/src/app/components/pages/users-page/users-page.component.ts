import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { TableBase } from '../../common/table-base/table-base';
import { UserFilter } from 'src/app/models/UserFilter';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { UserService } from 'src/app/services/UserService';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';
import { TableHeaderInfo } from '../../common/table/table-header/TableHeaderInfo';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class UsersPageComponent extends TableBase<User> implements OnInit {
  public headers: TableHeaderInfo<null>[] =
    [new TableHeaderInfo('Username', 'username-column', null, false),
    new TableHeaderInfo('First name', 'first-name-column', null, false),
    new TableHeaderInfo('Last name', 'last-name-column', null, false),
    new TableHeaderInfo('Email', 'email-column', null, false),
    new TableHeaderInfo('', 'permissions-column', null, false)];

  public isPermissionsEditingEnabled = false;
  public editingUser: User = null;

  public filter: UserFilter = new UserFilter(
    new PagingInfo(1, 20));

    constructor(
      private _userService: UserService) {
      super();
    }

  public getAction(filter: UserFilter): Observable<PagedResult<User>> {
    return this._userService.Get(filter);
  }

  public ngOnInit() {
    super.ngOnInit();
  }

  public onPermissionsButtonClick(user: User) {
    this.isPermissionsEditingEnabled = true;
    this.editingUser = user;
  }
}
