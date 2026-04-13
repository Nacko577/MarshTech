import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DeviceService } from '../../services/device.service';
import { UserService } from '../../services/user.service';
import { DeviceWriteDto } from '../../models/device.model';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-device-form',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './device-form.html',
  styleUrl: './device-form.css'
})
export class DeviceFormComponent implements OnInit {
  isEdit = false;
  deviceId: number | null = null;
  users = signal<User[]>([]);
  error = signal('');
  loading = signal(false);
  ready = signal(false);

  form: DeviceWriteDto = {
    name: '',
    manufacturer: '',
    type: 'Phone',
    operatingSystem: '',
    osVersion: '',
    processor: '',
    ramAmount: '',
    description: '',
    userId: undefined
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private deviceService: DeviceService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.userService.getAll().subscribe(users => this.users.set(users));

    const id = this.route.snapshot.paramMap.get('id');
    if (id && !isNaN(Number(id))) {
      this.isEdit = true;
      this.deviceId = Number(id);
      this.deviceService.getById(this.deviceId).subscribe({
        next: (device) => {
          this.form.name = device.name;
          this.form.manufacturer = device.manufacturer;
          this.form.type = device.type;
          this.form.operatingSystem = device.operatingSystem;
          this.form.osVersion = device.osVersion;
          this.form.processor = device.processor;
          this.form.ramAmount = device.ramAmount;
          this.form.description = device.description ?? '';
          this.form.userId = device.userId;
          this.ready.set(true);
        },
        error: () => {
          this.error.set('Failed to load device.');
          this.ready.set(true);
        }
      });
    } else {
      this.ready.set(true);
    }
  }

  onSubmit() {
    this.error.set('');
    this.loading.set(true);

    const dto: DeviceWriteDto = {
      ...this.form,
      userId: this.form.userId ? Number(this.form.userId) : undefined
    };

    if (this.isEdit && this.deviceId) {
      this.deviceService.update(this.deviceId, dto).subscribe({
        next: () => this.router.navigate(['/devices', this.deviceId]),
        error: (err) => {
          this.error.set(err.error?.message ?? 'Failed to update device.');
          this.loading.set(false);
        }
      });
    } else {
      this.deviceService.create(dto).subscribe({
        next: (created) => this.router.navigate(['/devices', created.id]),
        error: (err) => {
          this.error.set(err.error?.message ?? 'Failed to create device.');
          this.loading.set(false);
        }
      });
    }
  }
}
