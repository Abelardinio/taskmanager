import { NotificationsService } from 'angular2-notifications';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './BaseService';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, tap } from 'rxjs/operators';
import { Lookup } from '../models/Lookup';
import { PermissionsModel } from '../models/PermissionsModel';
import { Messages } from '../resources/messages';

@Injectable({
    providedIn: 'root',
})

/**
 * Provides functionality for api/task methods calls
 */
export class PermissionService extends BaseService {

    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient,
        localstorageAccessor: LocalStorageAccessor) {
        super(localstorageAccessor);
    }

    /**
     * Returns an observable of http get method which returns permissions lookup
     */
    public GetLookup(): Observable<Lookup[]> {
        return <Observable<Lookup[]>>this._httpClient
            .get(environment.AUTH_URL + '/users/permissions/lookup', { headers: this.headers })
            .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns permissions for user assigned to a project
     *
     * @param userId user Id
     * @param projectId project Id
     */
    public Get(userId: number, projectId: number): Observable<number[]> {
        return <Observable<number[]>>this._httpClient
            .get(environment.AUTH_URL + '/users/' + userId + '/permissions/' + projectId, { headers: this.headers })
            .pipe(catchError(this.handleError()));
    }

    /**
     * Returns an observable of http pu method which updates permission for a user
     *
     * @param userId user Id
     * @param model model with changed permissions
     */
    public Update(userId: number, model: PermissionsModel): Observable<Object> {
        return this._httpClient
            .put(environment.AUTH_URL + '/users/' + userId + '/permissions', JSON.stringify(model), { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Permisssions.Updated); }),
                catchError(this.handleError()));
    }
}
