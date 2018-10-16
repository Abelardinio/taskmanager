import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTaskPageComponent } from './add-task-page.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TimepickerComponent } from '../../common/timepicker/timepicker.component';
import { SimpleNotificationsModule } from 'angular2-notifications';

describe('AddTaskPageComponent', () => {
  let component: AddTaskPageComponent;
  let fixture: ComponentFixture<AddTaskPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ ReactiveFormsModule, NgSelectModule, FormsModule, SimpleNotificationsModule ],
      declarations: [ AddTaskPageComponent, TimepickerComponent]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTaskPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
