import { FormGroup } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { Messages } from 'src/app/resources/messages';


export abstract class FormBase<T> {
    public formIsValidated = false;
    public formIsLoading = false;

    protected abstract form: FormGroup;
    protected abstract notifications: NotificationsService;
    protected abstract submitAction(value: T): Observable<Object>;
    protected onSuccess(authResult) {}

    public onSubmit() {
        if (this.form.valid) {
            const result = this.form.value;

            this.formIsLoading = true;
            this.form.disable();
            this.submitAction(result)
                .pipe(finalize(() => this.onSubmitEnd()))
                .subscribe((authResult) => this.onSuccess(authResult));
        } else {
            this.formIsValidated = true;
            this.notifications.info(Messages.Common.FormValidationMessage);
        }
    }

    protected onSubmitEnd() {
        this.formIsValidated = false;
        this.formIsLoading = false;
        this.form.enable();
    }
}
