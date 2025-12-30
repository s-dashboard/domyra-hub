import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from './components/header/header';
import { Sidenav } from './components/sidenav/sidenav';
import { AppTitleService } from './components/services/app-title.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Sidenav],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  constructor(private readonly appTitle: AppTitleService) {
    this.appTitle.setTitle('Domyra - Hub');
  }
}
