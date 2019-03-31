import { Component, OnInit, Input } from '@angular/core';
import { TableHeaderInfo } from '../../common/table/table-header/TableHeaderInfo';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { TableBase } from '../../common/table-base/table-base';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';
import { Feature } from 'src/app/models/Feature';
import { FeatureFilter } from 'src/app/models/FeatureFilter';
import { FeatureService } from 'src/app/services/FeatureService';


@Component({
  selector: 'app-features-table',
  templateUrl: './features-table.component.html',
  styleUrls: ['./features-table.component.css']
})
export class FeaturesTableComponent extends TableBase<Feature> implements OnInit {
  @Input() public set projectId(value: number) {
    if (value === undefined) {
      return;
    }

    this.filter.ProjectId = value;
    this.onRefresh();
  }

  @Input() public projectIsSelected: boolean;

  public headers: TableHeaderInfo<null>[] =
    [new TableHeaderInfo('Feauture link', 'feature-link-column', null, false),
     new TableHeaderInfo('Name', 'name-column', null, false)];

  public filter: FeatureFilter = new FeatureFilter(
    new PagingInfo(1, 20));

  constructor(
    private _featureService: FeatureService) {
    super();
  }

  public getAction(filter: FeatureFilter): Observable<PagedResult<Feature>> {
    return this._featureService.Get(filter);
  }

  public ngOnInit() {
    this.fetchDataOnInit = !this.projectIsSelected;
    if (!this.projectIsSelected) {
      this.headers.push(new TableHeaderInfo('ProjectName', 'project-name-column', null, false));
    }
    super.ngOnInit();
  }
}
