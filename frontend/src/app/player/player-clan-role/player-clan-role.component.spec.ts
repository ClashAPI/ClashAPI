import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {PlayerClanRoleComponent} from './player-clan-role.component';

describe('PlayerClanRoleComponent', () => {
  let component: PlayerClanRoleComponent;
  let fixture: ComponentFixture<PlayerClanRoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerClanRoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerClanRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
