import { Component, WritableSignal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AppTitleService } from '../services/app-title.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  imports: [MatToolbarModule, MatIconModule, MatButtonModule],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  protected title!: WritableSignal<string>;

  constructor(private readonly appTitleService: AppTitleService) {
    this.title = this.appTitleService.title;
  }
}
