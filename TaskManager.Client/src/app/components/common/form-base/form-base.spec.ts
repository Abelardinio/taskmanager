import { Messages } from "src/app/resources/messages";
import { FormGroup } from "@angular/forms";
import { NotificationsService } from "angular2-notifications";
import { FormBase } from "./form-base";
import { Observable } from "rxjs";

class FormBaseTestClass  extends FormBase<any> {
    constructor(protected form: FormGroup,
                protected notifications: NotificationsService ){
        super();
    }
    protected submitAction(value: any): Observable<Object> {
        throw new Error("Method not implemented.");
    }

    public getFormIsValidated(){ return this.formIsValidated; }

    public submit(){
        this.onSubmit();
    }
}

describe('FormBase', () => {
    var component,
        formGroup,
        notifications;

    beforeEach(() => {
        formGroup = <FormGroup>{ valid: true };
        notifications = <NotificationsService>{ info: () => { } };
        component = new FormBaseTestClass(formGroup, notifications);
    });

    it('should become validated after submit click in case there are validation errors', () => {
        var notificationsSpy = spyOn(notifications, 'info');
            formGroup.valid = false;
        component.submit();
        expect(notificationsSpy).toHaveBeenCalledWith(Messages.Common.FormValidationMessage);
        expect(component.getFormIsValidated()).toBe(true);
    });
});