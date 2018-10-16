import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksGridFilterComponent } from './tasks-grid-filter.component';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../common/search/search.component';
import { DatepickerComponent } from '../../common/datepicker/datepicker.component';
import { NumberRangeComponent } from '../../common/number-range/number-range.component';
import { NgDatepickerModule } from 'ng2-datepicker';
import { NgSelectModule } from '@ng-select/ng-select';

describe('TasksGridFilterComponent', () => {
  let component: TasksGridFilterComponent;
  let fixture: ComponentFixture<TasksGridFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, NgDatepickerModule, NgSelectModule],
      declarations: [ TasksGridFilterComponent, SearchComponent, DatepickerComponent, NumberRangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasksGridFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
