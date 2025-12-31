import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WidgetInstall } from './widget-install';

describe('WidgetInstall', () => {
  let component: WidgetInstall;
  let fixture: ComponentFixture<WidgetInstall>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WidgetInstall]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WidgetInstall);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
