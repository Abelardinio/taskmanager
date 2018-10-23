import { Component, OnInit, forwardRef, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ValueAccessorBase } from '../value-accessor-base';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { Utils } from '../../../common/utils';

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
export class PagerComponent extends ValueAccessorBase<PagingInfo> implements OnInit, OnChanges {
  @Input() public pagesCount: number;
  public pageSizeArray: number[] = [20, 50, 100];
  public pageNumberArray: number[];

  public ngOnInit() {
    this.pageNumberArray = Utils.generateArray(this.pagesCount + 1);
    this.pageNumberArray.shift();
  }

  public ngOnChanges(changes: SimpleChanges): void {
    this.pageNumberArray = Utils.generateArray(this.pagesCount + 1);
    this.pageNumberArray.shift();
  }

  public onNumberChange(v: number) {
    this.initValue();
    this.value.Number = v;
  }

  public onSizeChange(v: number) {
    this.initValue();
    this.value.Size = v;
  }

  public onNextButtonClick() {
    if (this.value.Number < this.pagesCount) {
      this.initValue();
      this.value.Number++;
      this.onValueChange();
    }
  }

  public onPrevButtonClick() {
    if (this.value.Number > 1) {
      this.initValue();
      this.value.Number--;
      this.onValueChange();
    }
  }

  private initValue() {
    if (!this.value) {
      this.value = new PagingInfo(null, null);
    }
  }
}
