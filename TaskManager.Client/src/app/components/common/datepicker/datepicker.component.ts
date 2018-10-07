import { Component, OnInit } from '@angular/core';
import { DatepickerOptions } from 'ng2-datepicker';


@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.css']
})
export class DatepickerComponent implements OnInit {
  
  options: DatepickerOptions = {
    minYear: 1970,
    maxYear: 2030,
    displayFormat: 'MMM D[,] YYYY',
    barTitleFormat: 'MMMM YYYY',
    dayNamesFormat: 'dd',
    firstCalendarDay: 0, // 0 - Sunday, 1 - Monday
    placeholder: 'Date from...', // HTML input placeholder attribute (default: '')
    addClass: 'datepicker', // Optional, value to pass on to [ngClass] on the input field
  };
  
  constructor() { }

  ngOnInit() {
  }

}
