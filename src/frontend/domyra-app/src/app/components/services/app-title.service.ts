import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AppTitleService {
  readonly title = signal('');

  setTitle(title: string) {
    this.title.set(title);
  }
}
