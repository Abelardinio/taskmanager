import { TaskInfo } from '../models/TaskInfo';
import { TaskFilter } from '../models/TaskFilter';
import { NotificationsService } from 'angular2-notifications';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Messages } from '../resources/messages';
import { BaseService } from './BaseService';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class TaskService  extends BaseService{

    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient) {            
        super();
    }

    /**
     * Returns an observable of http post method which adds new task
     * @param taskInfo - information about task
     */
    public Add(taskInfo: TaskInfo): Observable<Object> {
        return this._httpClient.post(environment.API_URL + '/task', JSON.stringify(taskInfo), { headers: this.headers })
                               .pipe(tap(() => { this.notifications.success(Messages.Tasks.Added) }),
                                     catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of task
     * @param filter - task filter
     */
    public Get(filter: TaskFilter): Observable<Object> {
        let params = new HttpParams();
        params = params.append('taskId', filter.taskId.toString());
        params = params.append('count', filter.count.toString());
        params = params.append('type', filter.type.toString());


        return this._httpClient.get(environment.API_URL + '/task', { headers: this.headers, params: params })
                               .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http put method which changes task status to 'Completed'
     * @param taskId - task Id
     */
    public Complete(taskId: number): Observable<Object> {
        return this._httpClient.put(environment.API_URL + '/task/' + taskId + '/complete', {})
                               .pipe(tap(() => { this.notifications.success(Messages.Tasks.Completed) }),
                                     catchError(this.handleError()));
    }

    /**
     * Returns an observable of http delete method which changes task status to 'Deleted'
     * @param taskId - task Id
     */
    public Delete(taskId: number): Observable<Object> {
        return this._httpClient.delete(environment.API_URL + '/task/' + taskId, {})
                               .pipe(tap(() => { this.notifications.success(Messages.Tasks.Removed) }),
                                     catchError(this.handleError()));
    }
}