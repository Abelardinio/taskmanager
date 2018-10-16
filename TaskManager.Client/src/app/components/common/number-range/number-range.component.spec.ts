import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NumberRangeComponent } from './number-range.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';

describe('NumberRangeComponent', () => {
  let component: NumberRangeComponent;
  let fixture: ComponentFixture<NumberRangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, NgSelectModule ],
      declarations: [ NumberRangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NumberRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
