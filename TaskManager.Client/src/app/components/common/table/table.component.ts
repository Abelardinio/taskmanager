import { Component, OnInit, Input, ContentChild, TemplateRef, ElementRef, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  @Input() elements: any[];
  @Output() rowSelected = new EventEmitter<any>();
  @ContentChild('headerTemplate') headerTemplate: TemplateRef<ElementRef>;
  @ContentChild('rowTemplate') rowTemplate: TemplateRef<ElementRef>;
  selected: any = {};

  constructor() { }

  ngOnInit() {
  }

  onRowSelect(element: any, event) {
    if (event.target.className === 'btn') { return; }
    this.selected = element;
    this.rowSelected.emit(element);
  }
}
