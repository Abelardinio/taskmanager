import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Project } from 'src/app/models/Project';
import { ProjectService } from 'src/app/services/ProjectService';

@Component({
  selector: 'app-project-page',
  templateUrl: './project-page.component.html',
  styleUrls: ['./project-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class ProjectPageComponent implements OnInit {
  public project: Project;
  constructor(
    private _projectService: ProjectService,
    private _route: ActivatedRoute) { }

  public ngOnInit() {
    const projectId = Number(this._route.snapshot.paramMap.get('id'));
    this._projectService.GetById(projectId).subscribe(project => this.project = project);
  }
}
