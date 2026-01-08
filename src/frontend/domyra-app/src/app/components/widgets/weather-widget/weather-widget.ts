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
  hourlyForecast: Signal<HourlyForecast[] | undefined>;
  dailyForecast: Signal<DailyForecast[] | undefined>;

  constructor(private readonly weather: WeatherService) {
    this.localTemperature = toSignal(this.weather.currentLocalTemperature);
    this.hourlyForecast = toSignal(this.weather.hourlyForecast);
    this.dailyForecast = toSignal(this.weather.dailyForecast);
  }

  getWeatherIcon = (value: number) => weatherIcon(value);

}
