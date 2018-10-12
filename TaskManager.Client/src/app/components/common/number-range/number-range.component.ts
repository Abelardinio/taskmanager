import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ValueAccessorBase } from '../value-accessor-base';
import { NumberRange } from '../../../models/NumberRange';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import * as _ from 'lodash';

@Component({
  selector: 'app-number-range',
  templateUrl: './number-range.component.html',
  styleUrls: ['./number-range.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => NumberRangeComponent),
    }]
})
export class NumberRangeComponent extends ValueAccessorBase<NumberRange> implements OnInit {
  @Input() numberArray: number[];
  fromArray: number[];
  toArray: number[];
  ngOnInit() {
    this.fromArray = this.numberArray;
    this.toArray = this.numberArray;
  }

  onFromModelChange(v: number) {
    this.initValue();
    this.value.From = v;
    this.toArray = _.filter(this.numberArray, x => x > v);
  }

  onToModelChange(v: number) {
    this.initValue();
    this.value.To = v;
    this.fromArray = _.filter(this.numberArray, x => x < v);
  }

  initValue() {
    if (!this.value) {
      this.value = new NumberRange(null, null);
    }
  }
}
