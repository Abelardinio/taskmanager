import { async, ComponentFixture, TestBed, tick, fakeAsync } from '@angular/core/testing';
import { SearchComponent } from './search.component';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('SearchComponent', () => {
  let component: SearchComponent;
  let fixture: ComponentFixture<SearchComponent>;
  const value = "value";
  const emptyValue = "";

  const testComponentInput = (input) =>{
    component.value = input;
    fixture.detectChanges();

    return fixture.whenStable().then(() => {
      expect(fixture.debugElement.query(By.css('input')).nativeElement.value).toBe(input);
    });
  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule ],
      declarations: [ SearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should have render value in input element after its input value change', () => {
    expect(fixture.debugElement.query(By.css('input')).nativeElement.value).toBe(emptyValue);
    testComponentInput(value).then(() => { testComponentInput(emptyValue); });
  });

  it('should change its input value after value changes in input element', fakeAsync(() => {
    const inputElement = fixture.debugElement.query(By.css('input')).nativeElement;
    inputElement.value = value;
    inputElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    tick(400);
    expect(component.value).toBe(value);
  }));
});
