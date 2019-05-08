import { NotificationsService } from 'angular2-notifications';
import { HttpClient} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError} from 'rxjs/operators';
import { BaseService } from './BaseService';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';
import { Task } from '../models/Task';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class UserTasksService extends BaseService {

    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient,
        localstorageAccessor: LocalStorageAccessor) {
        super(localstorageAccessor);
    }

    /**
     * Returns an observable of http get method which returns a collection of user tasks
     */
    public Get(): Observable<Task[]> {
        return <Observable<Task[]>>this._httpClient
        .get(environment.HOME_URL + '/usertasks', { headers: this.headers, })
        .pipe(catchError(this.handleError()));
    }
}
