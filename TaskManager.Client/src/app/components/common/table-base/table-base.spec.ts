import { Observable } from 'rxjs';
import { TableBase } from './table-base';
import { BaseSortableFilter } from 'src/app/models/BaseFilter';
import { PagedResult } from 'src/app/models/PagedResult';
import * as _ from 'lodash';

class TableBaseTestClass extends TableBase<any> {
    public getActionObservable: Observable<PagedResult<any>>;
    public filter: BaseSortableFilter<number>;
    public objects: PagedResult<any> = new PagedResult<any>([{ Id: 1 }, { Id: 2 }, { Id: 3 }], 5);
    public onLoadComplete: () => void;
    constructor() {
        super();
        this.getActionObservable = new Observable(observer => {
            setTimeout(() => {
                observer.next(this.objects);
                observer.complete();
                this.onLoadComplete();
            }, 10);
        });
    }

    public RunRawAction(id: number, rawAction: () => Observable<Object>, onRawActionComplete: (value: Object) => void) {
        this.rowAjaxAction(
            id,
            rawAction,
            onRawActionComplete);
    }


    public getAction(filter: BaseSortableFilter<number>): Observable<PagedResult<any>> {
        return this.getActionObservable;
    }
}

describe('TableBase', () => {
    let component;

    beforeEach(() => {
        component = new TableBaseTestClass();
    });

    it('should load data on NgInit call', (done: DoneFn) => {
        component.onLoadComplete = () => {
            expect(component.rows.length).toBe(3);
            expect(component.pagesCount).toBe(5);
            done();
        };

        component.ngOnInit();
    });

    it('should init system info for each item', (done: DoneFn) => {
        component.onLoadComplete = () => {
            _.each(component.rows, (row) => {
                expect(row.SystemInfo.Version).toBe(1);
                expect(row.SystemInfo.IsLoading).toBe(false);
            });
            done();
        };

        component.ngOnInit();
    });

    it('should remove row by id', (done: DoneFn) => {
        component.onLoadComplete = () => {
            const rowToBeRemoved = _.find(component.rows, item => item.Model.Id === 2);

            expect(rowToBeRemoved.SystemInfo.Version).toBe(1);
            expect(_.find(component.rows, item => item.Model.Id === 2) === undefined).toBe(false);

            component.removeRow(2);

            expect(_.find(component.rows, item => item.Model.Id === 2) === undefined).toBe(true);
            expect(rowToBeRemoved.SystemInfo.Version).toBe(2);
            expect(component.rows.length).toBe(2);
            done();
        };

        component.ngOnInit();
    });

    it('should update row by id', (done: DoneFn) => {
        component.onLoadComplete = () => {
            const rowToBeUpdated = _.find(component.rows, item => item.Model.Id === 1);
            const handler = (data: any) => { data.Test = 'Test'; };

            expect(rowToBeUpdated.SystemInfo.Version).toBe(1);
            expect(_.find(component.rows, item => item.Model.Id === 1).Model.Test).toBe(undefined);
            component.updateRow(1, handler);
            expect(_.find(component.rows, item => item.Model.Id === 1).Model.Test).toBe('Test');
            expect(rowToBeUpdated.SystemInfo.Version).toBe(2);
            done();
        };

        component.ngOnInit();
    });

    it('should replace old rows on refresh', (done: DoneFn) => {
        component.rows = [{ Model: { Id: 5 } }, { Model: { Id: 7 } }];
        component.pagesCount = 3;

        component.onLoadComplete = () => {
            expect(_.find(component.rows, item => item.Model.Id === 5) === undefined).toBe(true);
            expect(_.find(component.rows, item => item.Model.Id === 7) === undefined).toBe(true);
            expect(component.rows.length).toBe(3);
            expect(component.pagesCount).toBe(5);
            done();
        };

        component.onRefresh();
    });

    it('should set page number to 1 and replace old rows on filter change', (done: DoneFn) => {
        component.rows = [{ Model: { Id: 5 } }, { Model: { Id: 7 } }];
        component.pagesCount = 3;
        component.filter = {
            PagingInfo: {
                Number: 5
            }
        };

        component.onLoadComplete = () => {
            expect(_.find(component.rows, item => item.Model.Id === 5) === undefined).toBe(true);
            expect(_.find(component.rows, item => item.Model.Id === 7) === undefined).toBe(true);
            expect(component.rows.length).toBe(3);
            expect(component.pagesCount).toBe(5);
            done();
        };

        component.onFilterChange();
        expect(component.filter.PagingInfo.Number).toBe(1);
    });

    it('should change selected row on row selected', () => {
        component.onRowSelected(undefined);
        expect(component.selectedRow).toBe(undefined);
        component.onRowSelected({ Model: { Id: 7 } });
        expect(component.selectedRow.Model.Id).toBe(7);
    });

    it('should set row to loading state on rowAjaxction', (done: DoneFn) => {
        const onRawActionComplete = () => { };

        const rawActionObservable = new Observable(observer => {
            setTimeout(() => {
                expect(_.find(component.rows, item => item.Model.Id === 3).SystemInfo.IsLoading).toBe(true);
                observer.next();
                observer.complete();
                setTimeout(() => {
                    expect(_.find(component.rows, item => item.Model.Id === 3).SystemInfo.IsLoading).toBe(false);
                    done();
                }, 10);
            }, 10);
        });

        component.onLoadComplete = () => {
            component.RunRawAction(3, () => rawActionObservable, onRawActionComplete);
        };

        component.ngOnInit();
    });
});
