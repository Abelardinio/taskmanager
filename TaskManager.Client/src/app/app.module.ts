import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgSelectModule } from '@ng-select/ng-select';
import { MatTableModule} from '@angular/material';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TimerComponent } from './components/common/timer/timer.component';
import { TaskDetailsComponent } from './components/panels/task-details/task-details.component';
import { TimepickerComponent } from './components/common/timepicker/timepicker.component';
import { TableComponent } from './components/common/table/table.component';
import { AddTaskPageComponent } from './components/pages/add-task-page/add-task-page.component';
import { TasksPageComponent } from './components/pages/tasks-page/tasks-page.component';
import { TasksGridFilterComponent } from './components/filters/tasks-grid-filter/tasks-grid-filter.component';

@NgModule({
  declarations: [
    AppComponent,
    TimerComponent,
    TaskDetailsComponent,
    TimepickerComponent,
    TableComponent,
    AddTaskPageComponent,
    TasksPageComponent,
    TasksGridFilterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MatTableModule,
    NgSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SimpleNotificationsModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
