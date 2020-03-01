import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerClanBadgeComponent } from './player-clan-badge.component';

describe('ClanBadgeComponent', () => {
  let component: PlayerClanBadgeComponent;
  let fixture: ComponentFixture<PlayerClanBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerClanBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerClanBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
