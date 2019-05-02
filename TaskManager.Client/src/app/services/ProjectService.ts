import { BaseService } from './BaseService';
import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';
import { ProjectInfo } from '../models/ProjectInfo';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Messages } from '../resources/messages';
import { Project } from '../models/Project';
import { PagedResult } from '../models/PagedResult';
import { ProjectFilter } from '../models/ProjectFilter';
import { Lookup } from '../models/Lookup';

@Injectable({
    providedIn: 'root',
})

export class ProjectService extends BaseService {
    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient,
        localstorageAccessor: LocalStorageAccessor) {
        super(localstorageAccessor);
    }

    /**
     * Returns an observable of http post method which adds new project
     * @param projectInfo - information about project
     */
    public Add(projectInfo: ProjectInfo): Observable<Object> {
        return this._httpClient.post(environment.API_URL + '/projects', JSON.stringify(projectInfo), { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Projects.Added); }),
                catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of projects
     */
    public Get(filter: ProjectFilter): Observable<PagedResult<Project>> {
        let params = new HttpParams();
        params = params.append('value', filter.Value ? filter.Value.toString() : '');
        params = params.append('pageSize', filter.PagingInfo.Size.toString());
        params = params.append('pageNumber', filter.PagingInfo.Number.toString());

        return <Observable<PagedResult<Project>>>this._httpClient
            .get(environment.API_URL + '/projects', { headers: this.headers, params: params })
            .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a project
     */
    public GetById(id: number): Observable<Project> {
        return <Observable<Project>>this._httpClient
            .get(environment.API_URL + '/projects/' + id, { headers: this.headers, })
            .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of project lookups
     */
    public GetLookup(): Observable<Lookup[]> {
        return <Observable<Lookup[]>>this._httpClient
        .get(environment.API_URL + '/projects/lookup', { headers: this.headers, })
        .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of project lookups which allow to add features
     */
    public GetLookupAddFeature(): Observable<Lookup[]> {
        return <Observable<Lookup[]>>this._httpClient
        .get(environment.API_URL + '/projects/lookup/addFeature', { headers: this.headers, })
        .pipe(catchError(this.handleError()));
    }
}
