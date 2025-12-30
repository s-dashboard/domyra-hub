import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { Devices } from './pages/devices/devices';
import { DevicesDetail } from './pages/devices/detail/devices-detail';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: Dashboard },
  { path: 'devices', component: Devices },
  { path: 'devices/add', component: DevicesDetail },
  { path: 'devices/:id', component: DevicesDetail },
];
