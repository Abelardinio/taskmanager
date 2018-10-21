import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TableHeaderComponent } from './table-header.component';
import { FormsModule } from '@angular/forms';
import { TableHeaderInfo } from './TableHeaderInfo';
import { SortingInfo } from 'src/app/models/SortingInfo';
import { SortingOrder } from 'src/app/models/enums/SortingOrder';
import { By } from '@angular/platform-browser';

describe('TableHeaderComponent', () => {
  let component: TableHeaderComponent<any>;
  let fixture: ComponentFixture<TableHeaderComponent<any>>;

  let headers: TableHeaderInfo<number>[] = [new TableHeaderInfo('First', 'First', 0),
                                            new TableHeaderInfo('Second', 'Second', 1),
                                            new TableHeaderInfo('Third', 'Third', 2),
                                            new TableHeaderInfo('Fourth', 'Fourth', null, false)];

  let value = new SortingInfo<number>(SortingOrder.Desc, 0);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule ],
      declarations: [ TableHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    value = new SortingInfo<number>(SortingOrder.Desc, 0);
    fixture = TestBed.createComponent(TableHeaderComponent);
    component = fixture.componentInstance;
    component.headers = headers;
    component.value = value;
    fixture.detectChanges();
  });

  it('should render headers depending on component headers input value', () => {
    expect(component).toBeTruthy();
    expect(fixture.debugElement.queryAll(By.css('th')).length).toBe(4);
    expect(fixture.debugElement.queryAll(By.css('th.First.sortable')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th.Second.sortable')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th.Third.sortable')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th.Fourth.sortable')).length).toBe(0);
    expect(fixture.debugElement.queryAll(By.css('th.First i.fa-angle-down')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);
  });

  it('should change sorting order on the same header click', () => {
    // assert
    expect(fixture.debugElement.queryAll(By.css('th.First i.fa-angle-down')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);

    // act
    const inputElement = fixture.debugElement.query(By.css('th.First.sortable')).nativeElement;
    inputElement.dispatchEvent(new Event('click'));
    fixture.detectChanges();

    // assert
    expect(fixture.debugElement.queryAll(By.css('th.First i.fa-angle-up')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);

     // act
    inputElement.dispatchEvent(new Event('click'));
    fixture.detectChanges();

    // assert
    expect(fixture.debugElement.queryAll(By.css('th.First i.fa-angle-down')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);
  });

  it('should change sorting column on the different headers click', () => {
    // assert
    expect(fixture.debugElement.queryAll(By.css('th.First i')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);

    // act
    const inputElement = fixture.debugElement.query(By.css('th.Second.sortable')).nativeElement;
    inputElement.dispatchEvent(new Event('click'));
    fixture.detectChanges();

    // assert
    expect(fixture.debugElement.queryAll(By.css('th.Second i')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);
  });

  it('should change component value on header click', () => {
    // assert
    expect(value.Column).toBe(0);
    expect(value.Order).toBe(1);

    // act
    const inputElement = fixture.debugElement.query(By.css('th.Second.sortable')).nativeElement;
    inputElement.dispatchEvent(new Event('click'));
    fixture.detectChanges();

    // assert
    expect(value.Column).toBe(1);
    expect(value.Order).toBe(1);

    // act
    inputElement.dispatchEvent(new Event('click'));
    fixture.detectChanges();

    // assert
    expect(value.Column).toBe(1);
    expect(value.Order).toBe(0);
  });

  it('should render changes on input value change', () => {
    // assert
    expect(fixture.debugElement.queryAll(By.css('th.First i')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);

    // act
    value.Column = 1;
    fixture.detectChanges();

    // assert
    expect(fixture.debugElement.queryAll(By.css('th.Second i')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);

    // act
    value.Column = 2;
    value.Order = 0;
    fixture.detectChanges();

    // assert
    expect(fixture.debugElement.queryAll(By.css('th.Third i.fa-angle-up')).length).toBe(1);
    expect(fixture.debugElement.queryAll(By.css('th i')).length).toBe(1);
  });
});
