import { Component, forwardRef } from '@angular/core';
import { ValueAccessorBase } from '../value-accessor-base';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => CheckboxComponent),
    }]
})
export class CheckboxComponent extends ValueAccessorBase<Boolean> {
  public isDisabled: Boolean = false;

  public setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
