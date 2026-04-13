import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model';

@Component({
  selector: 'app-device-detail',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './device-detail.html',
  styleUrl: './device-detail.css'
})
export class DeviceDetailComponent implements OnInit {
  device = signal<Device | null>(null);
  loading = signal(true);
  error = signal('');

  constructor(
    private route: ActivatedRoute,
    private deviceService: DeviceService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.deviceService.getById(id).subscribe({
      next: (data) => { this.device.set(data); this.loading.set(false); },
      error: () => { this.error.set('Device not found.'); this.loading.set(false); }
    });
  }
}
