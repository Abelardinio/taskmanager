import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTaskPageComponent } from './components/pages/add-task-page/add-task-page.component';
import { TasksPageComponent } from './components/pages/tasks-page/tasks-page.component';
import { AddUserPageComponent } from './components/pages/add-user-page/add-user-page.component';
import { UsersPageComponent } from './components/pages/users-page/users-page.component';
import { LoginPageComponent } from './components/pages/login-page/login-page.component';
import { LoginActivate } from './common/LoginActivate';

const routes: Routes = [
  { path: 'tasks', component: TasksPageComponent, canActivate: [LoginActivate] },
  { path: 'tasks/add', component: AddTaskPageComponent, canActivate: [LoginActivate] },
  { path: 'users', component: UsersPageComponent, canActivate: [LoginActivate] },
  { path: 'users/add', component: AddUserPageComponent, canActivate: [LoginActivate] },
  { path: '', redirectTo: '/tasks', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
