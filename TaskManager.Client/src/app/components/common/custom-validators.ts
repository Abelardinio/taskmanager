import { AbstractControl } from '../../../../node_modules/@angular/forms';
import { TimeSpan } from '../../models/TaskInfo';

export class CustomValidators {

    public static timepickerRequired(control: AbstractControl): { [key: string]: boolean } | null {
        const value = <TimeSpan>control.value;

        return value === null ||
            value === undefined ||
            (value.days === 0 && value.weeks === 0 && value.hours === 0) ?
            { 'required': true } : null;
    }
}
