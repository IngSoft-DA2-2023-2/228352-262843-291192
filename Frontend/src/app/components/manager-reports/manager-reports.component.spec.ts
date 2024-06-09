import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerReportsComponent } from './manager-reports.component';

describe('ReportsComponent', () => {
  let component: ManagerReportsComponent;
  let fixture: ComponentFixture<ManagerReportsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManagerReportsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ManagerReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
