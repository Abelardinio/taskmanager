import { TaskInfo } from '../models/TaskInfo';
import { NotificationsService } from 'angular2-notifications';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Messages } from '../resources/messages';
import { BaseService } from './BaseService';
import { TaskFilter } from '../models/TaskFilter';
import { PagedResult } from '../models/PagedResult';
import { Task } from '../models/Task';
import { MessagingServiceConnection } from '../common/MessagingServiceConnection';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class TaskService extends BaseService {

    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient,
        private _messagingConnection: MessagingServiceConnection,
        localstorageAccessor: LocalStorageAccessor) {
        super(localstorageAccessor);

        this._messagingConnection.init('/tasks');
    }

    /**
     * Returns an observable of http post method which adds new task
     * @param taskInfo - information about task
     */
    public Add(taskInfo: TaskInfo): Observable<Object> {
        return this._httpClient.post(environment.API_URL + '/task', JSON.stringify(taskInfo), { headers: this.headers })
                               .pipe(tap(() => { this.notifications.success(Messages.Tasks.Added); }),
                                     catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of task
     */
    public Get(filter: TaskFilter): Observable<PagedResult<Task>> {
        let params = new HttpParams();
        params = params.append('sortingColumn', filter.SortingInfo.Column.toString());
        params = params.append('sortingOrder', filter.SortingInfo.Order.toString());
        params = params.append('name', filter.Name ? filter.Name.toString() : '');
        params = params.append('addedFrom', filter.AddedFrom ? filter.AddedFrom.toDateString() : '');
        params = params.append('addedTo', filter.AddedTo ? filter.AddedTo.toDateString() : '');
        if (filter.Priority) {
            params = params.append('priorityFrom', filter.Priority.From ? filter.Priority.From.toString() : '');
            params = params.append('priorityTo', filter.Priority.To ? filter.Priority.To.toString() : '');
        }
        params = params.append('projectId', filter.ProjectId ? filter.ProjectId.toString() : '');
        params = params.append('featureId', filter.FeatureId ? filter.FeatureId.toString() : '');
        params = params.append('pageSize', filter.PagingInfo.Size.toString());
        params = params.append('pageNumber', filter.PagingInfo.Number.toString());

        return <Observable<PagedResult<Task>>>this._httpClient
                .get(environment.API_URL + '/task', { headers: this.headers, params: params })
                .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http put method which changes task status to 'Completed'
     * @param taskId - task Id
     */
    public Complete(taskId: number): Observable<Object> {
        return this._httpClient.put(environment.API_URL + '/task/' + taskId + '/complete', {}, { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Tasks.Completed); }),
                catchError(this.handleError()));
    }

    /**
     * Returns an observable of http delete method which changes task status to 'Removed'
     * @param taskId - task Id
     */
    public Delete(taskId: number): Observable<Object> {
        return this._httpClient.delete(environment.API_URL + '/task/' + taskId, { headers: this.headers })
                               .pipe(tap(() => { this.notifications.success(Messages.Tasks.Removed); }),
                                     catchError(this.handleError()));
    }

    /**
     * Returns an observable of http put method which assignes task to a user
     * @param taskId - task Id
     */
    public Assign(taskId: number): Observable<Object> {
        return this._httpClient.put(environment.API_URL + '/task/' + taskId + '/assign', {}, { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Tasks.Completed); }),
                catchError(this.handleError()));
    }

        /**
     * Returns an observable of http put method which unassignes task to a user
     * @param taskId - task Id
     */
    public Unassign(taskId: number): Observable<Object> {
        return this._httpClient.put(environment.API_URL + '/task/' + taskId + '/unassign', {}, { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Tasks.Completed); }),
                catchError(this.handleError()));
    }

    /**
     * Calls a handler when task was deleted
     *
     * @param fn event handler
     */
    public onTaskDeleted(fn: (id: number) => void) {
        this._messagingConnection.on('TASK_DELETED', fn);
    }

     /**
     * Calls a handler when task was deleted
     *
     * @param fn event handler
     */
    public onTaskCompleted(fn: (id: number) => void) {
        this._messagingConnection.on('TASK_COMPLETED', fn);
    }

     /**
     * Calls a handler when task was assigned
     *
     * @param fn event handler
     */
    public onTaskAssigned(fn: (task: Task) => void) {
        this._messagingConnection.on('TASK_ASSIGNED',
            message => fn(
                new Task(
                    message.taskId,
                    message.name,
                    message.added,
                    message.description,
                    message.priority,
                    message.status,
                    message.assignedUserId)));
    }

     /**
     * Calls a handler when task was unassigned
     *
     * @param fn event handler
     */
    public onTaskUnassigned(fn: (id: number) => void) {
        this._messagingConnection.on('TASK_UNASSIGNED', fn);
    }
}
