import { Component, forwardRef} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR} from '../../../../../node_modules/@angular/forms';
import { TimeSpan } from '../../../models/TaskInfo';
import { Utils } from '../utils';
import { Labels } from '../../../resources/labels';

@Component({
  selector: 'timepicker',
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

export class TimepickerComponent implements ControlValueAccessor {

  public daysArray = Utils.generateArray(7);
  public hoursArray = Utils.generateArray(24);
  public weeksArray = Utils.generateArray(52);
  public time: TimeSpan;
  isDisabled: Boolean = false;
  onChange;
  onTouched;

  constructor() { }

  ngOnInit() {
  }

  onValueChange() {
    this.onChange(this.time);
  }

  writeValue(obj: TimeSpan): void {
    this.time = obj;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  get labels(){ return Labels.TimePicker}
}

