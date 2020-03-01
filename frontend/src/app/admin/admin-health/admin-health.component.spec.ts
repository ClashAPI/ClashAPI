import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminHealthComponent } from './admin-health.component';

describe('AdminHealthComponent', () => {
  let component: AdminHealthComponent;
  let fixture: ComponentFixture<AdminHealthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminHealthComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminHealthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
