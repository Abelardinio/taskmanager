import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimerComponent } from './timer.component';

describe('TimerComponent', () => {
  let component: TimerComponent;
  let fixture: ComponentFixture<TimerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimerComponent ]
    })
    .compileComponents();
  }));

  it('should render placeholder if time is expired', () => {
    fixture = TestBed.createComponent(TimerComponent);
    component = fixture.componentInstance;

    var date = new Date();
    date.setDate(date.getDate() - 1);

    component.completionDate = date.toDateString();
    component.expiredPlaceholder = "expired";
    fixture.detectChanges();
    expect(fixture.debugElement.nativeElement.innerText).toBe("expired");
  });
});
