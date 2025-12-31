import { Component, Signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { Widget, widgetColumns } from '../../models/widget.model';
import { WidgetResponse } from '../../responses/widget.response';
import { WidgetsDataStore } from '../../datastores/widgets.datastore';
import { toSignal } from '@angular/core/rxjs-interop';
import { WidgetsService } from '../../services/widgets.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-widgets',
  imports: [MatTableModule, MatIconModule, MatButtonModule],
  templateUrl: './widgets.html',
  styleUrl: './widgets.scss',
})
export class Widgets {

  dataSource: Signal<WidgetResponse[]>;
  displayedColumns = widgetColumns;

  constructor(
    private readonly widgetData: WidgetsDataStore,
    private readonly widgetService: WidgetsService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {
    this.dataSource = toSignal(this.widgetData.widgets$, { initialValue: [] });
    this.widgetData.fetchAll({});
  }

  onInstallClick() {
    this.router.navigate(['install'], { relativeTo: this.route });
  }

  onActivateClick(widgetResponse: WidgetResponse) {
    this.widgetService.activate(widgetResponse);
  }
}
