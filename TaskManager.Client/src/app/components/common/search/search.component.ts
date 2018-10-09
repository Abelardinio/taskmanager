import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  @Input() placeholder: string;
  @Input() className: string;
  @Input() value: string;
  @Output() valueChange: EventEmitter<string> = new EventEmitter<string>();
  valueSubject: Subject<string> = new Subject<string>();
  constructor() { }

  ngOnInit() {
    this.valueSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()).subscribe(this.onValueChange);
  }

  onValueChange = (value: string) => {
    this.value = value;
    this.valueChange.emit(value);
  }
}
