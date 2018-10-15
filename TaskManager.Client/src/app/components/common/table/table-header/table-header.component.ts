import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TableHeaderInfo } from './TableHeaderInfo';
import { SortingOrder } from '../../../../models/enums/SortingOrder';

@Component({
  selector: 'app-table-header',
  templateUrl: './table-header.component.html',
  styleUrls: ['./table-header.component.css']
})
export class TableHeaderComponent implements OnInit {

  @Input() public headers: TableHeaderInfo[];
  @Input() public sortingOrder: SortingOrder;
  @Output() public sortingOrderChange: EventEmitter<SortingOrder> = new EventEmitter<SortingOrder>();
  @Input() public columnNumber: number;
  @Output() public columnNumberChange: EventEmitter<number> = new EventEmitter<number>();
  @Output() public sortingChange = new EventEmitter<any>();

  public ngOnInit() {
  }

  public onHeaderClick(header: TableHeaderInfo) {
    if (header.Sortable) {
      if (this.columnNumber === header.SortingNumber) {
        this.sortingOrder = this.sortingOrder === SortingOrder.Asc ? SortingOrder.Desc : SortingOrder.Asc;
      } else {
        this.columnNumber = header.SortingNumber;
        this.sortingOrder = SortingOrder.Desc;
      }

      this.sortingOrderChange.emit(this.sortingOrder);
      this.columnNumberChange.emit(this.columnNumber);
      this.sortingChange.emit();
    }
  }
}
