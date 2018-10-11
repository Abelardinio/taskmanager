import { Component, forwardRef, Input} from '@angular/core';
import { ValueAccessorBase } from '../value-accessor-base';
import { NumberRange } from '../../../models/NumberRange';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

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
export class NumberRangeComponent extends ValueAccessorBase<NumberRange> {
  @Input() numberArray: number[];
  ngOnInit() {
  }

  onFromModelChange(v:number){
    this.initValue();

    this.value.From = v;
  }

  onToModelChange(v:number){
    this.initValue();
    this.value.To = v;
  }

  initValue(){
    if (!this.value){
      this.value = new NumberRange(null,null);
    }
  }
}
