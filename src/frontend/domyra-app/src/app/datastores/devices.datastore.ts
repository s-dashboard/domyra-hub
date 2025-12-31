import { Injectable } from '@angular/core';
import { catchError, map, Observable, BehaviorSubject, tap } from 'rxjs';
import { DeviceResponse } from '../responses/device.response';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { SaveDeviceRequest } from '../requests/savedevice.request';
import { ErrorService } from '../services/error.service';
import { LoaderService } from '../services/loader.service';

@Injectable({ providedIn: 'root' })
export class DevicesDataStore {
  
  private source: string = `${environment.apiUri}/devices`;
  private _devices$: BehaviorSubject<DeviceResponse[]> = new BehaviorSubject<DeviceResponse[]>([]);
  private _selectedDevice$: BehaviorSubject<DeviceResponse | null> =
    new BehaviorSubject<DeviceResponse | null>(null);

  constructor(
    private readonly http: HttpClient,
    private readonly errors: ErrorService,
    private readonly loader: LoaderService
  ) {}

  readonly devices$ = this._devices$.asObservable();
  readonly selectedDevice$ = this._selectedDevice$.asObservable();

  fetchSingle(id: number) {
    this.loader.wrap(this.http
      .get<DeviceResponse>(`${this.source}/${id}`)
      .pipe(catchError((err) => this.errors.handleBadRequest(err)))
    , 'Fetch device..').subscribe((dto) => this._selectedDevice$.next(dto));
  }

  fetchAll() {
    this.loader.wrap(
      this.http
      .get<DeviceResponse[]>(`${this.source}`)
      .pipe(catchError((err) => this.errors.handleBadRequest(err)))
    ,'Fetching devices..').subscribe((dtos) => this._devices$.next(dtos));
  }

  save(request: SaveDeviceRequest): Observable<DeviceResponse | null> {
    const id = request?.id;
    const save$ = id ? this.update(request) : this.add(request);

    return this.loader.wrap(save$.pipe(tap(() => this.fetchAll())), `Saving device "${request.name}"`);
  }

  activate(id: number, activateText: string) {
    this.loader.wrap(this.http
      .put<null>(`${this.source}/${id}/activate`,null, {
        observe: 'response',
      })
      .pipe(
        tap(() => this.fetchAll()),
        catchError((err) => this.errors.handleBadRequest(err)),
      ),
      activateText
    ).subscribe(() => {
      console.log('activate/deactivate');
    });
  }

  private add(request: SaveDeviceRequest): Observable<DeviceResponse | null> {
    return this.http
      .post<DeviceResponse>(this.source, request)
      .pipe(catchError((err) => this.errors.handleBadRequest(err)));
  }

  private update(request: SaveDeviceRequest): Observable<DeviceResponse | null> {
    const id = request?.id;

    return this.http
      .put<null>(`${this.source}/${id}`, request, {
        observe: 'response',
      })
      .pipe(
        map(() => null),
        catchError((err) => this.errors.handleBadRequest(err)),
      );
  }
}
