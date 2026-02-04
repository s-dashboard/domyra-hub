import { CommonModule } from '@angular/common';
import { Component, Signal } from '@angular/core';
import { WeatherService, weatherIcon } from './services/weather.service';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-weather-widget',
  imports: [CommonModule],
  templateUrl: './weather-widget.html',
  styleUrl: './weather-widget.scss',
})
export class WeatherWidget {
  localTemperature: Signal<LocalTemperature | null | undefined>;
  currentHourForecast: Signal<HourlyForecast[] | undefined>;
  dailyForecast: Signal<DailyForecast[] | undefined>;

  constructor(private readonly weather: WeatherService) {
    this.localTemperature = toSignal(this.weather.currentLocalTemperature);
    this.currentHourForecast = toSignal(this.weather.currentHourForecast);
    this.dailyForecast = toSignal(this.weather.dailyForecast);
  }

  getWeatherIcon = (value: number) => weatherIcon(value);
}
