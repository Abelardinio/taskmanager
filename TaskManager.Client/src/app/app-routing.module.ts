import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTaskPageComponent } from './components/pages/add-task-page/add-task-page.component';
import { TasksPageComponent } from './components/pages/tasks-page/tasks-page.component';

const routes: Routes = [
  { path: 'tasks', component: TasksPageComponent },
  { path: 'tasks/add', component: AddTaskPageComponent },
  { path: '', redirectTo: '/tasks', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
