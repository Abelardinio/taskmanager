import { HttpHeaders } from '../../../node_modules/@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';
import { NotificationsService } from '../../../node_modules/angular2-notifications';
import { LocalStorageAccessor } from '../common/LocalStorageAccessor';
import { Messages } from '../resources/messages';

/**
 * Base class for common functionality of http requests
 */
export abstract class BaseService {
    protected headers: HttpHeaders;
    protected abstract notifications: NotificationsService;

    constructor(localstorageAccessor: LocalStorageAccessor) {
        this.headers = new HttpHeaders();
        this.headers = this.headers.append('Content-Type', 'application/json');
        this.headers = this.headers.append('Authorization', 'Bearer ' + localstorageAccessor.token);
    }

    /**
     * Http request error handler which notifies a user an error occured and rethrows that error
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    protected handleError<T>(operation = 'operation', result?: T) {
        return (data: any): Observable<T> => {
            if (data.error) {
                this.notifications.error(data.error.Message);
            } else if (data.status === 403) {
                this.notifications.error(Messages.Errors.Unauthorized);
            } else {
                this.notifications.error(Messages.Errors.Unknown);
            }

            throw data;
        };
    }
}
