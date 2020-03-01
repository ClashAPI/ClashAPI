import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCrAccountsComponent } from './user-cr-accounts.component';

describe('UserCrAccountsComponent', () => {
  let component: UserCrAccountsComponent;
  let fixture: ComponentFixture<UserCrAccountsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserCrAccountsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserCrAccountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
