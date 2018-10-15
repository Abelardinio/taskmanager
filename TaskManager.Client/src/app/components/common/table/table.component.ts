import { Component, OnInit, Input, ContentChild, TemplateRef, ElementRef, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  @Input() public elements: any[];
  @Output() public rowSelected = new EventEmitter<any>();
  @ContentChild('headerTemplate') public headerTemplate: TemplateRef<ElementRef>;
  @ContentChild('rowTemplate') public rowTemplate: TemplateRef<ElementRef>;
  public selected: any = {};

  public ngOnInit() {
  }

  public onRowSelect(element: any, event) {
    if (event.target.className === 'btn') { return; }
    this.selected = element;
    this.rowSelected.emit(element);
  }
}
