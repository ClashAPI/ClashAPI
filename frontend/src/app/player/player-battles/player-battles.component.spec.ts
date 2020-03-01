import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {PlayerBattlesComponent} from './player-battles.component';

describe('PlayerBattlesComponent', () => {
  let component: PlayerBattlesComponent;
  let fixture: ComponentFixture<PlayerBattlesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerBattlesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerBattlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
