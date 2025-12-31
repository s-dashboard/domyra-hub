import { Component } from '@angular/core';
import { WidgetGroup } from "../../components/widget-group/widget-group";

@Component({
  selector: 'app-dashboard',
  imports: [WidgetGroup],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {

}
