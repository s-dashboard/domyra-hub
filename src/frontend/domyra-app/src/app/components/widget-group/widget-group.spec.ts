import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WidgetGroup } from './widget-group';

describe('WidgetGroup', () => {
  let component: WidgetGroup;
  let fixture: ComponentFixture<WidgetGroup>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WidgetGroup]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WidgetGroup);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
