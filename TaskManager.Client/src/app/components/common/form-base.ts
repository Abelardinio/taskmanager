import { FormGroup } from '@angular/forms';
import { finalize } from '../../../../node_modules/rxjs/operators';
import { Messages } from '../../resources/messages';
import { NotificationsService } from '../../../../node_modules/angular2-notifications';
import { Observable } from '../../../../node_modules/rxjs';

export abstract class FormBase<T> {
    protected formIsValidated = false;
    protected formIsLoading = false;

    protected abstract form: FormGroup;
    protected abstract notifications: NotificationsService;
    protected abstract submitAction(value: T): Observable<Object>;

    protected onSubmit() {
        if (this.form.valid) {
            const result = this.form.value;

            this.form.disable();
            this.formIsLoading = true;
            this.submitAction(result)
                .pipe(finalize(() => this.onSubmitEnd()))
                .subscribe();
        } else {
            this.formIsValidated = true;
            this.notifications.info(Messages.Common.FormValidationMessage);
        }
    }


    protected onSubmitEnd() {
        this.formIsValidated = false;
        this.form.enable();
        this.formIsLoading = false;
    }
}
