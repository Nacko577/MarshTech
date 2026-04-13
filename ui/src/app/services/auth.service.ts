import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5274/api/auth';
  private _loggedIn = signal(!!localStorage.getItem('token'));

  readonly isLoggedIn = this._loggedIn.asReadonly();

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, { username, password }).pipe(
      tap(res => this.saveToken(res.token))
    );
  }

  register(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/register`, { username, password }).pipe(
      tap(res => this.saveToken(res.token))
    );
  }

  private saveToken(token: string) {
    localStorage.setItem('token', token);
    this._loggedIn.set(true);
  }

  logout() {
    localStorage.removeItem('token');
    this._loggedIn.set(false);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
