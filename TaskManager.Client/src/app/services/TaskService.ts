import { TaskInfo } from '../models/TaskInfo';
import { TaskFilter } from '../models/TaskFilter';
import { NotificationsService } from 'angular2-notifications';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Messages } from '../resources/messages';

@Injectable({
    providedIn: 'root',
})

export class TaskService {

    private _headers: HttpHeaders;


    constructor(
        private _notifications: NotificationsService,
        private _httpClient: HttpClient) {
        this._headers = new HttpHeaders();
        this._headers = this._headers.append('Content-Type', 'application/json');
    }

    public Add(taskInfo: TaskInfo): Observable<Object> {
        return this._httpClient.post(environment.API_URL + '/task', JSON.stringify(taskInfo), { headers: this._headers })
                               .pipe(tap(() => { this._notifications.success(Messages.Tasks.Added) }),
                                     catchError(this.handleError()));
    }

    public Get(filter: TaskFilter): Observable<Object> {
        let params = new HttpParams();
        params = params.append('taskId', filter.taskId.toString());
        params = params.append('count', filter.count.toString());
        params = params.append('type', filter.type.toString());


        return this._httpClient.get(environment.API_URL + '/task', { headers: this._headers, params: params })
                               .pipe(catchError(this.handleError()));
    }

    public Complete(taskId: number): Observable<Object> {
        return this._httpClient.put(environment.API_URL + '/task/' + taskId + '/complete', {})
                               .pipe(tap(() => { this._notifications.success(Messages.Tasks.Completed) }),
                                     catchError(this.handleError()));
    }

    public Delete(taskId: number): Observable<Object> {
        return this._httpClient.delete(environment.API_URL + '/task/' + taskId, {})
                               .pipe(tap(() => { this._notifications.success(Messages.Tasks.Removed) }),
                                     catchError(this.handleError()));
    }


    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    private handleError<T>(operation = 'operation', result?: T) {
        return (data: any): Observable<T> => {
            this._notifications.error(data.error.Message);
            throw data;
        };
    }
}