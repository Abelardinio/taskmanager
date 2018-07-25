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
import { TasksFormComponent } from './components/tasks-form/tasks-form.component';
import { AddTaskFormComponent } from './components/add-task-form/add-task-form.component';
import { TimerComponent } from './components/common/timer/timer.component';
import { TaskDetailsComponent } from './components/shared/task-details/task-details.component';
import { TimepickerComponent } from './components/common/timepicker/timepicker.component';

@NgModule({
  declarations: [
    AppComponent,
    TasksFormComponent,
    AddTaskFormComponent,
    TimerComponent,
    TaskDetailsComponent,
    TimepickerComponent
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
