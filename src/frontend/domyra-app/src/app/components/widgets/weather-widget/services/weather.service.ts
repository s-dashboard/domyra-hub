import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, first } from 'rxjs';
import { DevicesDataStore } from '../../../../datastores/devices.datastore';

@Injectable({ providedIn: 'root' })
export class WeatherService implements OnDestroy {
  constructor(
    private readonly http: HttpClient,
    private readonly dataStore: DevicesDataStore,
  ) {
    this.initHourlyRefresh();
    this.initMinuteRefresh(1);
  }

  private hourlyIntervalId?: number;
  private hourlyTimeoutId?: number;

  private minuteIntervalId?: number;
  private minuteTimeoutId?: number;

  private readonly kalix = {
    latitude: 65.85298,
    longitude: 23.15645,
  };

  private readonly weatherApi: string =
    `https://api.open-meteo.com/v1/forecast` +
    `?latitude=${this.kalix.latitude}` +
    `&longitude=${this.kalix.longitude}` +
    `&hourly=temperature_2m,weathercode` +
    `&daily=temperature_2m_min,temperature_2m_max,weathercode` +
    `&timezone=auto`;

  private _currentHourForecast$: BehaviorSubject<HourlyForecast[]> = new BehaviorSubject<
    HourlyForecast[]
  >([]);

  private _dailyForecast$: BehaviorSubject<DailyForecast[]> = new BehaviorSubject<DailyForecast[]>(
    [],
  );

  private _currentLocalTemperature$: BehaviorSubject<LocalTemperature | null> =
    new BehaviorSubject<LocalTemperature | null>(null);

  get currentHourForecast() {
    return this._currentHourForecast$.asObservable();
  }

  get dailyForecast() {
    return this._dailyForecast$.asObservable();
  }

  get currentLocalTemperature() {
    return this._currentLocalTemperature$.asObservable();
  }

  private initMinuteRefresh(minutes: number) {
    // Initial localtemp
    this.loadLocalTemperature();

    const now = new Date();

    // ms until the next aligned minute boundary
    const msUntilNextTick =
      (minutes - (now.getMinutes() % minutes)) * 60 * 1000 -
      now.getSeconds() * 1000 -
      now.getMilliseconds();

    this.minuteTimeoutId = window.setTimeout(() => {
      this.loadLocalTemperature();

      this.minuteIntervalId = window.setInterval(
        () => {
          this.loadLocalTemperature();
        },
        minutes * 60 * 1000,
      );
    }, msUntilNextTick);
  }

  private loadLocalTemperature() {
    this.dataStore
      .getLastKnownDeviceValue(1)
      .pipe(first())
      .subscribe((val: string | null) => {
        if (val) {
          const parsed = JSON.parse(val);
          this._currentLocalTemperature$.next({
            current: Number(parsed.current),
            max: Number(parsed.max),
            min: Number(parsed.min),
          });
        } else {
          this._currentLocalTemperature$.next(null);
        }
      });
  }

  private initHourlyRefresh() {
    // Initial load
    this.loadWeatherForecast();

    // always start on full hour
    const now = new Date();
    now.setMinutes(0, 0, 0);
    now.setHours(now.getHours() + 1);

    const msUntilNextHour =
      (60 - now.getMinutes()) * 60 * 1000 - now.getSeconds() * 1000 - now.getMilliseconds();

    this.hourlyTimeoutId = window.setTimeout(() => {
      this.loadWeatherForecast();

      this.hourlyIntervalId = window.setInterval(
        () => {
          this.loadWeatherForecast();
        },
        60 * 60 * 1000,
      );
    }, msUntilNextHour);
  }

  private loadWeatherForecast() {
    const now = new Date();

    this.http.get<OpenMeteoResponse>(this.weatherApi).subscribe((data) => {
      this.updateCurrentHourForecast(data, now);
      this.updateDailyForecast(data);
    });
  }

  private updateDailyForecast(data: OpenMeteoResponse) {
    const dailyForecast = data.daily.time.slice(0, 5).map((date, i) => ({
      date,
      min: data.daily.temperature_2m_min[i],
      max: data.daily.temperature_2m_max[i],
      weatherCode: data.daily.weathercode[i],
    }));
    this._dailyForecast$.next(dailyForecast);
  }

  private updateCurrentHourForecast(data: OpenMeteoResponse, now: Date) {
    const startIndex = data.hourly.time.findIndex((t) => new Date(t) >= now) - 1;
    const hourlyForecast = data.hourly.time.slice(startIndex, startIndex + 1).map((time, i) => ({
      time,
      temperature: data.hourly.temperature_2m[startIndex + i],
      weatherCode: data.hourly.weathercode[startIndex + i],
    }));

    this._currentHourForecast$.next(hourlyForecast);
  }

  ngOnDestroy() {
    if (this.hourlyTimeoutId) {
      clearTimeout(this.hourlyTimeoutId);
    }
    if (this.hourlyIntervalId) {
      clearInterval(this.hourlyIntervalId);
    }
    if (this.minuteTimeoutId) {
      clearInterval(this.minuteTimeoutId);
    }
    if (this.minuteIntervalId) {
      clearTimeout(this.minuteIntervalId);
    }
  }
}

export function weatherIcon(code: number): string {
  if (code === 0) return '☀️';
  if ([1, 2, 3].includes(code)) return '⛅';
  if ([45, 48].includes(code)) return '🌫';
  if ([51, 53, 55, 61, 63, 65].includes(code)) return '🌧';
  if ([71, 73, 75].includes(code)) return '❄️';
  if ([95, 96, 99].includes(code)) return '⛈';
  return '🌥';
}
