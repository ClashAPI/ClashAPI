import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {ClanTypeComponent} from './clan-type.component';

describe('ClanTypeComponent', () => {
  let component: ClanTypeComponent;
  let fixture: ComponentFixture<ClanTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClanTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClanTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
