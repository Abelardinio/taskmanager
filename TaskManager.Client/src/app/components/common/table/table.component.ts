import { Component, OnInit, Input, ContentChild, TemplateRef, ElementRef, Output, EventEmitter } from '@angular/core';
import { trigger, transition, query, style, stagger, animate, keyframes, state } from '@angular/animations';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
  animations: [
    trigger('valueUpdated', [
      state('void => *', style({ opacity: 1, backgroundColor: 'white' })),
      transition('void => *', []),
      transition('* => *', [
        animate(500, style({ opacity: 0.3, backgroundColor: 'green' }))
      ])
    ])
  ]
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
    this.selected = element.Model;
    this.rowSelected.emit(element);
  }
}
