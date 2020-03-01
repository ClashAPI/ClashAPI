import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QueryPlayerComponent } from './query-player.component';

describe('QueryPlayerComponent', () => {
  let component: QueryPlayerComponent;
  let fixture: ComponentFixture<QueryPlayerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueryPlayerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueryPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
