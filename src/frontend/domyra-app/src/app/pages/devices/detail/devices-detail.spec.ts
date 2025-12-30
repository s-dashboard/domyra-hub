import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DevicesDetail } from './devices-detail';

describe('DevicesDetail', () => {
  let component: DevicesDetail;
  let fixture: ComponentFixture<DevicesDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DevicesDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevicesDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
