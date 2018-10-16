import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimepickerComponent } from './timepicker.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule, NgSelectComponent } from '@ng-select/ng-select';
import { TimeSpan } from 'src/app/models/TaskInfo';
import { By } from '@angular/platform-browser';

describe('TimepickerComponent', () => {
  let component: TimepickerComponent;
  let fixture: ComponentFixture<TimepickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, NgSelectModule ],
      declarations: [ TimepickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimepickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should have render', () => {
    component.value = new TimeSpan(1,0,0);
    const element = fixture.debugElement.query(By.directive(NgSelectComponent));
    expect(element.nativeElement.value).toBe('1');
  });
});
