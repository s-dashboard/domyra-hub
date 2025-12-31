import { Component, Signal } from '@angular/core';
import { WidgetDefinition } from '../../models/widget.model';
import { toSignal } from '@angular/core/rxjs-interop';
import { WidgetsDataStore } from '../../datastores/widgets.datastore';
import { map } from 'rxjs';
import { MatGridListModule } from '@angular/material/grid-list';
import { CommonModule } from '@angular/common';
import { WidgetsService } from '../../services/widgets.service';

@Component({
  selector: 'app-widget-group',
  imports: [MatGridListModule, CommonModule],
  templateUrl: './widget-group.html',
  styleUrl: './widget-group.scss',
})
export class WidgetGroup {
  widgets: Signal<WidgetDefinition[]>;

  constructor(private readonly widgetService: WidgetsService) {
    this.widgets = toSignal(this.widgetService.activeWidgets$, { initialValue: [] });
    this.widgetService.fetchAllActive();
  }
  
}
