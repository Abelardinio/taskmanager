import { NotificationsService } from 'angular2-notifications';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError} from 'rxjs/operators';
import { BaseService } from './BaseService';
import { LoginModel } from '../models/LoginModel';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class LoginService extends BaseService {

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
    public Auth(loginModel: LoginModel): Observable<Object> {
        return this._httpClient.post(environment.AUTH_URL + '/login', JSON.stringify(loginModel), { headers: this.headers })
            .pipe(catchError(this.handleError()));
    }
}
