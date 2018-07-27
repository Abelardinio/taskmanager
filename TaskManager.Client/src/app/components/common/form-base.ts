import { FormGroup } from '@angular/forms';
import { finalize } from '../../../../node_modules/rxjs/operators';
import { Messages } from '../../resources/messages';
import { NotificationsService } from '../../../../node_modules/angular2-notifications';
import { Observable } from '../../../../node_modules/rxjs';

export abstract class FormBase {
    protected formIsValidated = false;
    protected formIsLoading = false;

    constructor(){}

    protected abstract notifications: NotificationsService;
    protected abstract submitAction(value:any) : Observable<Object>;
    protected abstract form: FormGroup;

    protected onSubmit() {
        if (this.form.valid) {
            let result = this.form.value;

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