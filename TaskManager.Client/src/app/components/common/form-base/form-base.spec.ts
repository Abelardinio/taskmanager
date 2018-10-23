import { Messages } from 'src/app/resources/messages';
import { FormGroup } from '@angular/forms';
import { NotificationsService } from 'angular2-notifications';
import { FormBase } from './form-base';
import { Observable } from 'rxjs';

class FormBaseTestClass extends FormBase<any> {
    constructor(protected form: FormGroup,
        protected notifications: NotificationsService) {
        super();

        this.submitActionObservable = new Observable(observer => {
            setTimeout(() => {
                observer.complete();
            }, 10);
        });
    }

    private submitActionObservable: Observable<Object>;

    protected submitAction(value: any): Observable<Object> {
        return this.submitActionObservable;
    }

    public getFormIsValidated() { return this.formIsValidated; }

    public getFormIsLoading() { return this.formIsLoading; }

    public submitButtonClick() {
        this.onSubmit();
    }
}

describe('FormBase', () => {
    let component,
        formGroup,
        notifications;

    beforeEach(() => {
        formGroup = <FormGroup>{ valid: true, disable: () => { }, enable: () => { } };
        notifications = <NotificationsService>{ info: () => { } };
        component = new FormBaseTestClass(formGroup, notifications);
    });

    it('should become validated after submit click in case there are validation errors', () => {
        const notificationsSpy = spyOn(notifications, 'info');

        formGroup.valid = false;
        component.submitButtonClick();
        expect(notificationsSpy).toHaveBeenCalledWith(Messages.Common.FormValidationMessage);
        expect(component.getFormIsValidated()).toBe(true);
    });

    it('should set disabled form during submitting it to the server and enable it after', (done: DoneFn) => {
        const formGroupSpy = spyOn(formGroup, 'disable').and.callFake(() => {
            expect(component.getFormIsLoading()).toBe(true);
        });

        spyOn(formGroup, 'enable').and.callFake(() => {
            expect(formGroupSpy).toHaveBeenCalledTimes(1);
            expect(component.getFormIsLoading()).toBe(false);
            expect(component.getFormIsValidated()).toBe(false);
            done();
        });

        formGroup.valid = true;
        component.submitButtonClick();
    });
});
