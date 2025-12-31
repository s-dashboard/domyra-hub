import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, catchError, map, Observable, tap } from 'rxjs';
import { WidgetResponse } from '../responses/widget.response';
import { LoaderService } from '../services/loader.service';
import { HttpClient } from '@angular/common/http';
import { ErrorService } from '../services/error.service';
import { SaveWidgetRequest } from '../requests/savewidget.request';

@Injectable({ providedIn: 'root' })
export class WidgetsDataStore {
  private source: string = `${environment.apiUri}/widgets`;
  private _widgets$: BehaviorSubject<WidgetResponse[]> = new BehaviorSubject<WidgetResponse[]>([]);
  private _selectedWidget$: BehaviorSubject<WidgetResponse | null> =
    new BehaviorSubject<WidgetResponse | null>(null);

  constructor(
    private readonly http: HttpClient,
    private readonly loader: LoaderService,
    private readonly errors: ErrorService,
  ) {}

  readonly widgets$ = this._widgets$.asObservable();
  readonly selectedWidgets$ = this._selectedWidget$.asObservable();

  fetchSingle(id: number) {
    this.loader
      .wrap(
        this.http
          .get<WidgetResponse>(`${this.source}/${id}`)
          .pipe(catchError((err) => this.errors.handleBadRequest(err))),
        'Fetch widget..',
      )
      .subscribe((dto) => this._selectedWidget$.next(dto));
  }

  fetchAll(query: {
    state?: number
  }) {
    this.loader
      .wrap(
        this.http
          .get<WidgetResponse[]>(`${this.source}`, {
            params: query
          })
          .pipe(catchError((err) => this.errors.handleBadRequest(err))),
        'Fetching devices..',
      )
      .subscribe((dtos) => this._widgets$.next(dtos));
  }

  save(request: SaveWidgetRequest): Observable<WidgetResponse | null> {
    const alias = request?.alias;
    const save$ = alias ? this.update(request) : this.add(request);

    return this.loader.wrap(
      save$.pipe(tap(() => this.fetchAll({}))),
      `Saving widget "${request.alias}"`,
    );
  }

  activate(alias: string, activateText: string) {
    this.loader
      .wrap(
        this.http
          .put<null>(`${this.source}/${alias}/activate`, null, {
            observe: 'response',
          })
          .pipe(
            tap(() => this.fetchAll({})),
            catchError((err) => this.errors.handleBadRequest(err)),
          ),
        activateText,
      )
      .subscribe(() => {
        console.log('activate/deactivate');
      });
  }

  installWidget(formData: FormData): Observable<Object> {
    return this.loader.wrap(this.http.post(`${this.source}/install`, formData));
  }

  private add(request: SaveWidgetRequest): Observable<WidgetResponse | null> {
    return this.http
      .post<WidgetResponse>(this.source, request)
      .pipe(catchError((err) => this.errors.handleBadRequest(err)));
  }

  private update(request: SaveWidgetRequest): Observable<WidgetResponse | null> {
    const alias = request?.alias;

    return this.http
      .put<null>(`${this.source}/${alias}`, request, {
        observe: 'response',
      })
      .pipe(
        map(() => null),
        catchError((err) => this.errors.handleBadRequest(err)),
      );
  }
}
