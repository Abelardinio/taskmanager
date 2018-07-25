import { Component, forwardRef, Directive } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, Validator, FormControl } from '../../../../../node_modules/@angular/forms';
import { TimeSpan } from '../../../models/TaskInfo';

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

export class TimepickerComponent implements ControlValueAccessor{

  public daysArray = this._generateArray(7);
  public hoursArray = this._generateArray(24);
  public weeksArray = this._generateArray(52);
  public time: TimeSpan
  isDisabled: Boolean = false;
  onChange;
  onTouched;

  constructor() { }

  ngOnInit() {
  }

  get value(): TimeSpan {
    return this.time;
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

  private _generateArray(n: number): Array<number> {
    return Array.apply(null, { length: n }).map(Function.call, Number);
  }
}

