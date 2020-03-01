import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteClansComponent } from './favorite-clans.component';

describe('FavoriteClansComponent', () => {
  let component: FavoriteClansComponent;
  let fixture: ComponentFixture<FavoriteClansComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavoriteClansComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoriteClansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
