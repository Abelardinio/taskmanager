import { Component, OnInit, Input, forwardRef  } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ValueAccessorBase } from '../value-accessor-base';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => SearchComponent),
    }]
})
export class SearchComponent extends ValueAccessorBase<string> implements OnInit {
  @Input() public placeholder: string;
  @Input() public className: string;
  public valueSubject: Subject<string> = new Subject<string>();

  public ngOnInit() {
    this.valueSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()).subscribe(this.onValueChanged);
  }

  public onValueChanged = (value: string) => {
    this.value = value;
  }
}
