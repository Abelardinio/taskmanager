import { Component, OnInit, forwardRef, Input } from '@angular/core';
import { ValueAccessorBase } from '../value-accessor-base';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { Utils } from '../utils';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => PagerComponent),
    }]
})
export class PagerComponent extends ValueAccessorBase<PagingInfo> implements OnInit {
  @Input() pagesCount: number;
  pageSizeArray: number[] = [20, 50, 100];
  pageNumberArray: number[];
  ngOnInit() {
    this.pageNumberArray = Utils.generateArray(this.pagesCount);
    this.pageNumberArray.shift();
  }

  onNumberChange(v: number) {
    this.initValue();
    this.value.Number = v;
  }

  onSizeChange(v: number) {
    this.initValue();
    this.value.Size = v;
  }

  onNextButtonClick() {
    if (this.value.Number < this.pagesCount - 1) {
      this.initValue();
      this.value.Number++;
      this.onValueChange();
    }
  }

  onPrevButtonClick() {
    if (this.value.Number > 1) {
      this.initValue();
      this.value.Number--;
      this.onValueChange();
    }
  }

  initValue() {
    if (!this.value) {
      this.value = new PagingInfo(null, null);
    }
  }
}
