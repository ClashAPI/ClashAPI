import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteDecksComponent } from './favorite-decks.component';

describe('FavoriteDecksComponent', () => {
  let component: FavoriteDecksComponent;
  let fixture: ComponentFixture<FavoriteDecksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavoriteDecksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoriteDecksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
