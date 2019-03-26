import { BaseService } from './BaseService';
import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Messages } from '../resources/messages';
import { PagedResult } from '../models/PagedResult';
import { FeatureFilter } from '../models/FeatureFilter';
import { Feature } from '../models/Feature';
import { FeatureInfo } from '../models/FeatureInfo';

@Injectable({
    providedIn: 'root',
})

export class FeatureService extends BaseService {
    constructor(
        protected notifications: NotificationsService,
        private _httpClient: HttpClient,
        localstorageAccessor: LocalStorageAccessor) {
        super(localstorageAccessor);
    }

    /**
     * Returns an observable of http post method which adds new feature
     * @param featureInfo - information about feature
     */
    public Add(featureInfo: FeatureInfo): Observable<Object> {
        return this._httpClient.post(environment.API_URL + '/features', JSON.stringify(featureInfo), { headers: this.headers })
            .pipe(tap(() => { this.notifications.success(Messages.Features.Added); }),
                catchError(this.handleError()));
    }

    /**
     * Returns an observable of http get method which returns a collection of features
     */
    public Get(filter: FeatureFilter): Observable<PagedResult<Feature>> {
        let params = new HttpParams();
        params = params.append('value', filter.Value ? filter.Value.toString() : '');
        params = params.append('pageSize', filter.PagingInfo.Size.toString());
        params = params.append('pageNumber', filter.PagingInfo.Number.toString());

        return <Observable<PagedResult<Feature>>>this._httpClient
            .get(environment.API_URL + '/features', { headers: this.headers, params: params })
            .pipe(catchError(this.handleError()));
    }
}
