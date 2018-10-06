import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TableHeaderInfo } from './TableHeaderInfo';
import { SortingOrder } from '../../../../models/enums/SortingOrder';

@Component({
  selector: 'app-table-header',
  templateUrl: './table-header.component.html',
  styleUrls: ['./table-header.component.css']
})
export class TableHeaderComponent implements OnInit {

  @Input() headers: TableHeaderInfo[];
  @Input() sortingOrder: SortingOrder;  
  @Output() sortingOrderChange:EventEmitter<SortingOrder> = new EventEmitter<SortingOrder>();
  @Input() columnNumber: number;
  @Output() columnNumberChange:EventEmitter<number> = new EventEmitter<number>();
  @Output() sortingChange = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }

  onHeaderClick(header : TableHeaderInfo){
    if(header.Sortable){
      if(this.columnNumber === header.SortingNumber){
          this.sortingOrder = this.sortingOrder === SortingOrder.Asc ? SortingOrder.Desc : SortingOrder.Asc;
      }else{
        this.columnNumber = header.SortingNumber;
        this.sortingOrder = SortingOrder.Desc;
      }

      this.sortingOrderChange.emit(this.sortingOrder);
      this.columnNumberChange.emit(this.columnNumber);
      this.sortingChange.emit();
    }
  }
}
