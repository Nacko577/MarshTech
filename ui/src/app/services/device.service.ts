import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device, DeviceWriteDto } from '../models/device.model';

@Injectable({ providedIn: 'root' })
export class DeviceService {
  private apiUrl = 'http://localhost:5274/api/devices';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Device[]> {
    return this.http.get<Device[]>(this.apiUrl);
  }

  getById(id: number): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/${id}`);
  }

  create(dto: DeviceWriteDto): Observable<Device> {
    return this.http.post<Device>(this.apiUrl, dto);
  }

  update(id: number, dto: DeviceWriteDto): Observable<Device> {
    return this.http.put<Device>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, { observe: 'response' });
  }
}
