import { Injectable } from '@angular/core';
import { WidgetsDataStore } from '../datastores/widgets.datastore';
import { WidgetDefinition } from '../models/widget.model';
import { BehaviorSubject, filter, firstValueFrom, Observable, take } from 'rxjs';
import { WidgetLoaderService } from './widgetloader.service';
import { WidgetResponse } from '../responses/widget.response';

@Injectable({ providedIn: 'root' })
export class WidgetsService {
  private readonly _activeWidgets$ = new BehaviorSubject<WidgetDefinition[]>([]);

  constructor(
    private readonly dataStore: WidgetsDataStore,
    private readonly widgetLoader: WidgetLoaderService,
  ) {}

  readonly activeWidgets$ = this._activeWidgets$.asObservable();

  install(formData: FormData): Observable<Object> {
    return this.dataStore.installWidget(formData);
  }

  activate(widget: WidgetResponse): void {
    let activateText = `Activating widget "${widget.alias}"`;

    if (widget.state === 1) {
      activateText = `Deactivating widget "${widget.alias}"`;
    }

    this.dataStore.activate(widget.alias!, activateText);
  }

  async fetchAllActive() {
    this.dataStore.fetchAll({
      state: 1
    });
    
    const dtos = await firstValueFrom(
      this.dataStore.widgets$.pipe(
        filter((dtos) => dtos.length > 0),
        take(1),
      ),
    );

    const widgets = await Promise.all(dtos.map((dto) => this.widgetLoader.load(dto)));
    this._activeWidgets$.next(widgets);
  }
}
