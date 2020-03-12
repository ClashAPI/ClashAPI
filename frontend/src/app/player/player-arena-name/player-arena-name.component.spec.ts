import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerArenaNameComponent } from './player-arena-name.component';

describe('PlayerArenaNameComponent', () => {
  let component: PlayerArenaNameComponent;
  let fixture: ComponentFixture<PlayerArenaNameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerArenaNameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerArenaNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
