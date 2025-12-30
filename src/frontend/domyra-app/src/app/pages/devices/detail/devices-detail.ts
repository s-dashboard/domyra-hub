import { Component, effect, Signal } from '@angular/core';
import { DevicesDataStore } from '../../../datastores/devices.datastore';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, map, switchMap, tap } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop';
import { DeviceResponse } from '../../../responses/device.response';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { SaveDeviceRequest } from '../../../requests/savedevice.request';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-devices-detail',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './devices-detail.html',
  styleUrl: './devices-detail.scss',
})
export class DevicesDetail {
  device$: Signal<DeviceResponse | null>;

  protected form: FormGroup;

  constructor(
    private readonly deviceData: DevicesDataStore,
    private readonly route: ActivatedRoute,
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly snackBar: MatSnackBar,
  ) {
    this.form = this.fb.group({
      id: null,
      name: '',
      description: '',
      state: 0,
    });

    effect(() => {
      const device = this.device$();
      if (!device) return;

      this.form.patchValue(device, { emitEvent: false });
    });

    this.device$ = toSignal(
      this.route.paramMap.pipe(
        map((params) => params.get('id')),
        filter((id): id is string => id !== null),
        tap((id) => this.deviceData.fetchSingle(Number(id))),
        switchMap(() => this.deviceData.selectedDevice$),
      ),
      { initialValue: null },
    );
  }

  onGobackClick() {
    this.router.navigate(['..'], { relativeTo: this.route });
  }

  onSubmitForm() {
    if (this.form.valid) {
      const request: SaveDeviceRequest = this.form.getRawValue();
      this.deviceData.save(request).subscribe((isNew) => {
        this.snackBar.open('Device saved successfully', 'Close', {
          duration: 3000,
        });

        if (isNew) {
          this.onGobackClick();
        } else {
          this.form.updateValueAndValidity();
          this.form.markAsPristine();
        }
      });
    }
  }
}
