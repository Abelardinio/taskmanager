import { Component, OnInit, forwardRef, Input } from '@angular/core';
import { DatepickerOptions } from 'ng2-datepicker';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { ValueAccessorBase } from '../value-accessor-base';


@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => DatepickerComponent),
    }
  ]
})
export class DatepickerComponent extends ValueAccessorBase<Date> implements OnInit {
  @Input() public placeholder: string;

  public options: DatepickerOptions = {
    minYear: 1970,
    maxYear: 2030,
    displayFormat: 'MMM D[,] YYYY',
    barTitleFormat: 'MMMM YYYY',
    dayNamesFormat: 'dd',
    firstCalendarDay: 0, // 0 - Sunday, 1 - Monday
    placeholder: 'Date from...', // HTML input placeholder attribute (default: '')
    addClass: 'datepicker', // Optional, value to pass on to [ngClass] on the input field
  };

  public ngOnInit() {
    this.options.placeholder = this.placeholder;
  }

}
