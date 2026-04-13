import { Component, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class RegisterComponent {
  username = '';
  password = '';
  confirmPassword = '';
  error = signal('');
  loading = signal(false);

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    if (this.password.length < 8 || !/\d/.test(this.password) || !/[^a-zA-Z0-9]/.test(this.password)) {
      this.error.set('Password must be at least 8 characters and include a number and a special character.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      this.error.set('Passwords do not match.');
      return;
    }

    this.error.set('');
    this.loading.set(true);

    this.authService.register(this.username, this.password).subscribe({
      next: () => this.router.navigate(['/devices']),
      error: (err) => {
        this.error.set(err.error?.message ?? 'Registration failed.');
        this.loading.set(false);
      }
    });
  }
}
