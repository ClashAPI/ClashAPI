import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QueryClanComponent } from './query-clan.component';

describe('QueryClanComponent', () => {
  let component: QueryClanComponent;
  let fixture: ComponentFixture<QueryClanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueryClanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueryClanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
