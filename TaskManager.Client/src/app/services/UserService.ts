import { NotificationsService } from 'angular2-notifications';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Messages } from '../resources/messages';
import { BaseService } from './BaseService';
import { UserInfo } from '../models/UserInfo';
import { UserFilter } from '../models/UserFilter';
import { PagedResult } from '../models/PagedResult';
import { User } from '../models/User';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class UserService extends BaseService {

    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient,
        localstorageAccessor: LocalStorageAccessor) {
        super(localstorageAccessor);
    }

    /**
     * Returns an observable of http post method which adds new user
     * @param userInfo - information about task
     */
    public Add(userInfo: UserInfo): Observable<Object> {
        return this._httpClient.post(environment.AUTH_URL + '/users', JSON.stringify(userInfo), { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Users.Added); }),
                catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of users
     */
    public Get(filter: UserFilter): Observable<PagedResult<User>> {
        let params = new HttpParams();
        params = params.append('value', filter.Value ? filter.Value.toString() : '');
        params = params.append('pageSize', filter.PagingInfo.Size.toString());
        params = params.append('pageNumber', filter.PagingInfo.Number.toString());

        return <Observable<PagedResult<User>>>this._httpClient
                .get(environment.AUTH_URL + '/users', { headers: this.headers, params: params })
                .pipe(catchError(this.handleError()));
    }
}
