import { NotificationsService } from 'angular2-notifications';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Messages } from '../resources/messages';
import { BaseService } from './BaseService';
import { UserInfo } from '../models/UserInfo';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class UserService extends BaseService {

    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient) {
        super();
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
}
