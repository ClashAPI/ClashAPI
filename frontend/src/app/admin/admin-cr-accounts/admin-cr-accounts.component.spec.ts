import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {AdminCrAccountsComponent} from './admin-cr-accounts.component';

describe('AdminCrAccountsComponent', () => {
  let component: AdminCrAccountsComponent;
  let fixture: ComponentFixture<AdminCrAccountsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCrAccountsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCrAccountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
