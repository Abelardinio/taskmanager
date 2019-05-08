import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgDatepickerModule } from 'ng2-datepicker';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TimerComponent } from './components/common/timer/timer.component';
import { TimepickerComponent } from './components/common/timepicker/timepicker.component';
import { TableComponent } from './components/common/table/table.component';
import { AddTaskPageComponent } from './components/pages/add-task-page/add-task-page.component';
import { AddUserPageComponent } from './components/pages/add-user-page/add-user-page.component';
import { TasksPageComponent } from './components/pages/tasks-page/tasks-page.component';
import { TasksGridFilterComponent } from './components/filters/tasks-grid-filter/tasks-grid-filter.component';
import { TableHeaderComponent } from './components/common/table/table-header/table-header.component';
import { SearchComponent } from './components/common/search/search.component';
import { DatepickerComponent } from './components/common/datepicker/datepicker.component';
import { NumberRangeComponent } from './components/common/number-range/number-range.component';
import { PagerComponent } from './components/common/pager/pager.component';
import { MessagingServiceConnection } from './common/MessagingServiceConnection';
import { UsersPageComponent } from './components/pages/users-page/users-page.component';
import { UsersGridFilterComponent } from './components/filters/users-grid-filter/users-grid-filter.component';
import { LoginPageComponent } from './components/pages/login-page/login-page.component';
import { ProjectsPageComponent } from './components/pages/projects-page/projects-page.component';
import { ProjectsGridFilterComponent } from './components/filters/projects-grid-filter/projects-grid-filter.component';
import { AddProjectPageComponent } from './components/pages/add-project-page/add-project-page.component';
import { FeaturesPageComponent } from './components/pages/features-page/features-page.component';
import { FeaturesGridFilterComponent } from './components/filters/features-grid-filter/features-grid-filter.component';
import { AddFeaturePageComponent } from './components/pages/add-feature-page/add-feature-page.component';
import { ProjectPageComponent } from './components/pages/project-page/project-page.component';
import { FeaturesTableComponent } from './components/tables/features-table/features-table.component';
import { FeaturePageComponent } from './components/pages/feature-page/feature-page.component';
import { TasksTableComponent } from './components/tables/tasks-table/tasks-table.component';
import { ModalWindowComponent } from './components/common/modal-window/modal-window.component';
import { ProjectPermissionModalComponent } from './components/modals/project-permission-modal/project-permission-modal.component';
import { CheckboxComponent } from './components/common/checkbox/checkbox.component';
import { HomePageComponent } from './components/pages/home-page/home-page.component';

@NgModule({
  declarations: [
    AppComponent,
    TimerComponent,
    TimepickerComponent,
    TableComponent,
    AddTaskPageComponent,
    AddUserPageComponent,
    TasksPageComponent,
    TasksGridFilterComponent,
    TableHeaderComponent,
    SearchComponent,
    DatepickerComponent,
    NumberRangeComponent,
    PagerComponent,
    UsersPageComponent,
    UsersGridFilterComponent,
    LoginPageComponent,
    ProjectsPageComponent,
    ProjectsGridFilterComponent,
    AddProjectPageComponent,
    FeaturesPageComponent,
    FeaturesGridFilterComponent,
    AddFeaturePageComponent,
    ProjectPageComponent,
    FeaturesTableComponent,
    FeaturePageComponent,
    TasksTableComponent,
    ModalWindowComponent,
    ProjectPermissionModalComponent,
    CheckboxComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    NgSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgDatepickerModule,
    SimpleNotificationsModule.forRoot()
  ],
  providers: [MessagingServiceConnection],
  bootstrap: [AppComponent]
})
export class AppModule { }
