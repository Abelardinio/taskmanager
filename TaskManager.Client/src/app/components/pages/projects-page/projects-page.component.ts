import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/Project';
import { TableHeaderInfo } from '../../common/table/table-header/TableHeaderInfo';
import { ProjectFilter } from 'src/app/models/ProjectFilter';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { TableBase } from '../../common/table-base/table-base';
import { ProjectService } from 'src/app/services/ProjectService';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';

@Component({
  selector: 'app-projects-page',
  templateUrl: './projects-page.component.html',
  styleUrls: ['./projects-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class ProjectsPageComponent extends TableBase<Project> implements OnInit {
  public headers: TableHeaderInfo<null>[] =
    [ new TableHeaderInfo('Project Link', 'project-link-column', null, false),
      new TableHeaderInfo('Name', 'name-column', null, false)];

  public filter: ProjectFilter = new ProjectFilter(
    new PagingInfo(1, 20));

  constructor(
    private _projectService: ProjectService) {
    super();
  }

  public getAction(filter: ProjectFilter): Observable<PagedResult<Project>> {
    return this._projectService.Get(filter);
  }

  public ngOnInit() {
    super.ngOnInit();
  }
}
