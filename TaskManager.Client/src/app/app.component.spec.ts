import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TasksPageComponent } from './components/pages/tasks-page/tasks-page.component';
import { AddTaskPageComponent } from './components/pages/add-task-page/add-task-page.component';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { TasksGridFilterComponent } from './components/filters/tasks-grid-filter/tasks-grid-filter.component';
import { TableComponent } from './components/common/table/table.component';
import { TableHeaderComponent } from './components/common/table/table-header/table-header.component';
import { TimerComponent } from './components/common/timer/timer.component';
import { PagerComponent } from './components/common/pager/pager.component';
import { TaskDetailsComponent } from './components/panels/task-details/task-details.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgSelectModule } from '@ng-select/ng-select';
import { HttpClientModule } from '@angular/common/http';
import { NgDatepickerModule } from 'ng2-datepicker';
import { TimepickerComponent } from './components/common/timepicker/timepicker.component';
import { SearchComponent } from './components/common/search/search.component';
import { DatepickerComponent } from './components/common/datepicker/datepicker.component';
import { NumberRangeComponent } from './components/common/number-range/number-range.component';
import { RouterTestingModule } from '@angular/router/testing';
describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes(
          [{ path: 'tasks', component: TasksPageComponent },
          { path: 'tasks/add', component: AddTaskPageComponent },
          { path: '', redirectTo: '/tasks', pathMatch: 'full' }]
        ),
        SimpleNotificationsModule,
        BrowserModule,
        AppRoutingModule,
        FormsModule,
        BrowserAnimationsModule,
        NgSelectModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgDatepickerModule,],
      declarations: [
        AppComponent,
        TasksPageComponent,
        AddTaskPageComponent,
        TasksGridFilterComponent,
        TableComponent,
        TableHeaderComponent,
        TimerComponent,
        PagerComponent,
        TaskDetailsComponent,
        TimepickerComponent,
        SearchComponent,
        DatepickerComponent,
        NumberRangeComponent
      ],
    }).compileComponents();
  }));
  // it('should create the app', async(() => {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   const app = fixture.debugElement.componentInstance;
  //   expect(app).toBeTruthy();
  // }));
  // it(`should have as title 'app'`, async(() => {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   const app = fixture.debugElement.componentInstance;
  //   expect(app.title).toEqual('app');
  // }));
  // it('should render title in a h1 tag', async(() => {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   fixture.detectChanges();
  //   const compiled = fixture.debugElement.nativeElement;
  //   expect(compiled.querySelector('h1').textContent).toContain('Welcome to task-manager-client!');
  // }));
});


