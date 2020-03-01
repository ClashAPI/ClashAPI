import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {PlayerChestsComponent} from './player-chests.component';

describe('PlayerChestsComponent', () => {
  let component: PlayerChestsComponent;
  let fixture: ComponentFixture<PlayerChestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerChestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerChestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
