import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksGridFilterComponent } from './tasks-grid-filter.component';

describe('TasksGridFilterComponent', () => {
  let component: TasksGridFilterComponent;
  let fixture: ComponentFixture<TasksGridFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TasksGridFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasksGridFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
