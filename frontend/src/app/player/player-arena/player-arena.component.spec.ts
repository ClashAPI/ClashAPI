import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {PlayerArenaComponent} from './player-arena.component';

describe('PlayerArenaComponent', () => {
  let component: PlayerArenaComponent;
  let fixture: ComponentFixture<PlayerArenaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerArenaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerArenaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
