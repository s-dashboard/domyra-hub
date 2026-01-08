import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-clock-widget',
  imports: [],
  templateUrl: './clock-widget.html',
  styleUrl: './clock-widget.scss',
})
export class ClockWidget {
  readonly time = signal(this.format(new Date()));
  readonly today = signal(this.formatDate(new Date()));
  private timerId?: number;

  ngOnInit() {
    this.timerId = window.setInterval(() => {
      this.time.set(this.format(new Date()));
    }, 1000);
  }

  ngOnDestroy() {
    if (this.timerId) {
      clearInterval(this.timerId);
    }
  }

  private formatDate(date: Date): string {
    return date.toDateString();
  }

  private format(date: Date): string {
    return date.toLocaleTimeString([], {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false, // force 24h
    });
  }
}
