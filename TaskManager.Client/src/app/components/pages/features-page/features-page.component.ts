import { Component, OnInit } from '@angular/core';
import { TableHeaderInfo } from '../../common/table/table-header/TableHeaderInfo';
import { PagingInfo } from 'src/app/models/PagingInfo';
import { TableBase } from '../../common/table-base/table-base';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';
import { Feature } from 'src/app/models/Feature';
import { FeatureFilter } from 'src/app/models/FeatureFilter';
import { FeatureService } from 'src/app/services/FeatureService';

@Component({
  selector: 'app-features-page',
  templateUrl: './features-page.component.html',
  styleUrls: ['./features-page.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class FeaturesPageComponent extends TableBase<Feature> implements OnInit {
  public headers: TableHeaderInfo<null>[] =
    [new TableHeaderInfo('Name', 'name-column', null, false)];

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
    super.ngOnInit();
  }
}
