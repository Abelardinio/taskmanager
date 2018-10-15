import { Component, OnInit, Input, forwardRef} from '@angular/core';
import { TableHeaderInfo } from './TableHeaderInfo';
import { SortingOrder } from '../../../../models/enums/SortingOrder';
import { ValueAccessorBase } from '../../value-accessor-base';
import { SortingInfo } from 'src/app/models/SortingInfo';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-table-header',
  templateUrl: './table-header.component.html',
  styleUrls: ['./table-header.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => TableHeaderComponent),
    }]
})
export class TableHeaderComponent<T> extends ValueAccessorBase<SortingInfo<T>> implements OnInit {

  @Input() public headers: TableHeaderInfo<T>[];

  public ngOnInit() {
  }

  public onHeaderClick(header: TableHeaderInfo<T>) {
    if (header.Sortable) {
      if (this.value.Column === header.SortingNumber) {
        this.value.Order = this.value.Order === SortingOrder.Asc ? SortingOrder.Desc : SortingOrder.Asc;
      } else {
        this.value.Column = header.SortingNumber;
        this.value.Order = SortingOrder.Desc;
      }

      this.onValueChange();
    }
  }
}
