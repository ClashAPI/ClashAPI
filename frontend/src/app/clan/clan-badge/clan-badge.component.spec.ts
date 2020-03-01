import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClanBadgeComponent } from './clan-badge.component';

describe('ClanBadgeComponent', () => {
  let component: ClanBadgeComponent;
  let fixture: ComponentFixture<ClanBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClanBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClanBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
