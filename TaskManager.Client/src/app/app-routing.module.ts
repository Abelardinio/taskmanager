import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksFormComponent } from './components/tasks-form/tasks-form.component';
import { AddTaskFormComponent } from './components/add-task-form/add-task-form.component';

const routes: Routes = [
  { path: 'tasks', component: TasksFormComponent },
  { path: 'tasks/add', component: AddTaskFormComponent },
  { path: 'tasks/:id', component: TasksFormComponent },
  { path: '', redirectTo: '/tasks', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
