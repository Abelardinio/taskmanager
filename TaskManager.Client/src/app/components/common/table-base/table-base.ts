import { OnInit } from '@angular/core';
import { BaseFilter } from 'src/app/models/BaseFilter';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { IRowModel } from 'src/app/models/IRowModel';

export abstract class TableBase<TRowModel extends IRowModel, TSortingColumn> implements OnInit {

    protected abstract filter: BaseFilter<TSortingColumn>;
    public isGridRefreshing: Boolean = false;
    public pagesCount: number;
    public rows: TRowModel[];

    protected abstract getAction(filter: BaseFilter<TSortingColumn>): Observable<PagedResult<TRowModel>>;

    public ngOnInit(): void {
        this.fetchData();
    }

    public removeRow(id: number): void {
        _.remove(this.rows, (row) => row.Id === id);
    }

    public getRow(id: number): TRowModel {
        return _.find(this.rows, (row) => row.Id === id);
    }

    public onRefresh() {
        this.fetchData();
    }

    public onFilterChange() {
        this.filter.PagingInfo.Number = 1;
        this.fetchData();
    }

    private fetchData(): void {
        this.isGridRefreshing = true;
        this.getAction(this.filter)
            .pipe(finalize(() => { this.isGridRefreshing = false; }))
            .subscribe(
                data => {
                    this.rows = [];
                    this.pagesCount = data.PagesCount;
                    this.rows.push(...data.Items);
                });
    }
}
