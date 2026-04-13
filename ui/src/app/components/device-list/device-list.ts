import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model';

@Component({
  selector: 'app-device-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './device-list.html',
  styleUrl: './device-list.css'
})
export class DeviceListComponent implements OnInit {
  devices = signal<Device[]>([]);
  loading = signal(true);
  error = signal('');

  constructor(private deviceService: DeviceService) {}

  ngOnInit() {
    this.loadDevices();
  }

  loadDevices() {
    this.loading.set(true);
    this.deviceService.getAll().subscribe({
      next: (data) => { this.devices.set(data); this.loading.set(false); },
      error: () => { this.error.set('Failed to load devices.'); this.loading.set(false); }
    });
  }

  deleteDevice(id: number) {
    if (!confirm('Are you sure you want to delete this device?')) return;

    this.deviceService.delete(id).subscribe({
      next: () => this.loadDevices(),
      error: () => this.error.set('Failed to delete device.')
    });
  }
}
