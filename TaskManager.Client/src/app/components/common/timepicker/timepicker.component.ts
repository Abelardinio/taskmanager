import { Component, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '../../../../../node_modules/@angular/forms';
import { TimeSpan } from '../../../models/TaskInfo';
import { Utils } from '../utils';
import { Labels } from '../../../resources/labels';
import { ValueAccessorBase } from '../value-accessor-base';

@Component({
  selector: 'app-timepicker',
  templateUrl: './timepicker.component.html',
  styleUrls: ['./timepicker.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => TimepickerComponent),
    }
  ]
})

export class TimepickerComponent extends ValueAccessorBase<TimeSpan> {

  public daysArray = Utils.generateArray(7);
  public hoursArray = Utils.generateArray(24);
  public weeksArray = Utils.generateArray(52);
  isDisabled: Boolean = false;

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  get labels() { return Labels.TimePicker; }
}

