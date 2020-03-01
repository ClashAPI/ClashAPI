import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoritePlayersComponent } from './favorite-players.component';

describe('FavoritePlayersComponent', () => {
  let component: FavoritePlayersComponent;
  let fixture: ComponentFixture<FavoritePlayersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavoritePlayersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoritePlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
