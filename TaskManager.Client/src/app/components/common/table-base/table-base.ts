import { OnInit } from '@angular/core';
import { BaseFilter } from 'src/app/models/BaseFilter';
import { Observable } from 'rxjs';
import { PagedResult } from 'src/app/models/PagedResult';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { IRowModel } from 'src/app/models/IRowModel';

class RowInfo<TRowModel> {
    constructor(model: TRowModel) {
        this.Model = model;
        this.SystemInfo = {
            Version: 1,
            IsLoading: false
        };
    }
    public Model: TRowModel;
    public SystemInfo: {
        Version: number,
        IsLoading: boolean
    };
}

export abstract class TableBase<TRowModel extends IRowModel> implements OnInit {

    protected abstract filter: BaseFilter;
    protected fetchDataOnInit: Boolean = true;
    public isGridRefreshing: Boolean = false;
    public pagesCount: number;
    public rows: RowInfo<TRowModel>[];
    public selectedRow: RowInfo<TRowModel>;

    protected abstract getAction(filter: BaseFilter): Observable<PagedResult<TRowModel>>;

    public ngOnInit(): void {
        if (this.fetchDataOnInit) {
            this.fetchData();
        }
    }

    public removeRow(id: number): void {
        const row = this.getRow(id);

        if (row) {
            _.remove(this.rows, row => row.Model.Id === id);
            row.SystemInfo.Version++;
        }
    }

    public updateRow(id: number, updateHandler: (model: TRowModel) => void) {
        const row = this.getRow(id);

        if (row) {
            updateHandler(row.Model);
            row.SystemInfo.Version++;
        }
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
                    this.rows.push(...this.getRowInfo(data.Items));
                });
    }

    public onRowSelected(row: RowInfo<TRowModel>) {
        if (row) {
            this.selectedRow = row;
        }
    }

    protected rowAjaxAction(id: number, action: () => Observable<Object>, onSuccess: (value: Object) => void) {
        const row = this.getRow(id);
        if (row.SystemInfo.IsLoading) { return; }
        row.SystemInfo.IsLoading = true;

        action().pipe(finalize(() => { row.SystemInfo.IsLoading = false; })).subscribe(onSuccess);
    }

    private getRow(id: number): RowInfo<TRowModel> {
        return _.find(this.rows, (row) => row.Model.Id === id);
    }

    private getRowInfo(items: TRowModel[]): RowInfo<TRowModel>[] {
        return _.map(items, item => new RowInfo<TRowModel>(item));
    }
}
