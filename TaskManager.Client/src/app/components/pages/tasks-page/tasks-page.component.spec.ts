import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksPageComponent } from './tasks-page.component';
import { TasksGridFilterComponent } from '../../filters/tasks-grid-filter/tasks-grid-filter.component';
import { TableComponent } from '../../common/table/table.component';
import { TableHeaderComponent } from '../../common/table/table-header/table-header.component';
import { TimerComponent } from '../../common/timer/timer.component';
import { PagerComponent } from '../../common/pager/pager.component';
import { TaskDetailsComponent } from '../../panels/task-details/task-details.component';

describe('TasksPageComponent', () => {
  let component: TasksPageComponent;
  let fixture: ComponentFixture<TasksPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TasksPageComponent, TasksGridFilterComponent, TableComponent, TableHeaderComponent, TimerComponent, PagerComponent, TaskDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasksPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
