import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  @Input() public placeholder: string;
  @Input() public className: string;
  @Input() public value: string;
  @Output() public valueChange: EventEmitter<string> = new EventEmitter<string>();
  public valueSubject: Subject<string> = new Subject<string>();
  constructor() { }

  public ngOnInit() {
    this.valueSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()).subscribe(this.onValueChange);
  }

  public onValueChange = (value: string) => {
    this.value = value;
    this.valueChange.emit(value);
  }
}
