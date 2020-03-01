import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QueryInfoComponent } from './query-info.component';

describe('QueryInfoComponent', () => {
  let component: QueryInfoComponent;
  let fixture: ComponentFixture<QueryInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueryInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueryInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
