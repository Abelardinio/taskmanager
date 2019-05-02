import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTaskPageComponent } from './components/pages/add-task-page/add-task-page.component';
import { TasksPageComponent } from './components/pages/tasks-page/tasks-page.component';
import { AddUserPageComponent } from './components/pages/add-user-page/add-user-page.component';
import { UsersPageComponent } from './components/pages/users-page/users-page.component';
import { LoginPageComponent } from './components/pages/login-page/login-page.component';
import { LoginActivate } from './common/LoginActivate';
import { ProjectsPageComponent } from './components/pages/projects-page/projects-page.component';
import { AddProjectPageComponent } from './components/pages/add-project-page/add-project-page.component';
import { FeaturesPageComponent } from './components/pages/features-page/features-page.component';
import { AddFeaturePageComponent } from './components/pages/add-feature-page/add-feature-page.component';
import { ProjectPageComponent } from './components/pages/project-page/project-page.component';
import { FeaturePageComponent } from './components/pages/feature-page/feature-page.component';
import { SiteAdministratorActivate } from './common/SiteAdministratorActivate';
import { ProjectsCreatorActivate } from './common/ProjectsCreatorActivate';

const routes: Routes = [
  { path: 'tasks', component: TasksPageComponent, canActivate: [LoginActivate] },
  { path: 'tasks/add', component: AddTaskPageComponent, canActivate: [LoginActivate] },
  { path: 'users', component: UsersPageComponent, canActivate: [LoginActivate, SiteAdministratorActivate] },
  { path: 'users/add', component: AddUserPageComponent, canActivate: [LoginActivate, SiteAdministratorActivate] },
  { path: 'projects', component: ProjectsPageComponent, canActivate: [LoginActivate] },
  { path: 'projects/add', component: AddProjectPageComponent, canActivate: [LoginActivate, ProjectsCreatorActivate] },
  { path: 'projects/:id', component: ProjectPageComponent, canActivate: [LoginActivate] },
  { path: 'features', component: FeaturesPageComponent, canActivate: [LoginActivate] },
  { path: 'features/add', component: AddFeaturePageComponent, canActivate: [LoginActivate] },
  { path: 'features/:id', component: FeaturePageComponent, canActivate: [LoginActivate] },
  { path: '', redirectTo: '/tasks', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
