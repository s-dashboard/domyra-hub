import { Component, Signal } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { deviceColumns } from '../../models/device.model';
import { DevicesDataStore } from '../../datastores/devices.datastore';
import { toSignal } from '@angular/core/rxjs-interop';
import { DeviceResponse } from '../../responses/device.response';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-devices',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './devices.html',
  styleUrl: './devices.scss',
})
export class Devices {
  dataSource: Signal<DeviceResponse[]>;
  displayedColumns = deviceColumns;

  constructor(
    private readonly deviceData: DevicesDataStore,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
  ) {
    this.dataSource = toSignal(this.deviceData.devices$, { initialValue: [] });
    this.deviceData.fetchAll();
  }

  onAddClick() {
    this.router.navigate(['add'], { relativeTo: this.route });
  }
}
