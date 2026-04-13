import { User } from './user.model';

export interface Device {
  id: number;
  name: string;
  manufacturer: string;
  type: 'Phone' | 'Tablet';
  operatingSystem: string;
  osVersion: string;
  processor: string;
  ramAmount: string;
  description?: string;
  userId?: number;
  user?: User;
}

export interface DeviceWriteDto {
  name: string;
  manufacturer: string;
  type: 'Phone' | 'Tablet';
  operatingSystem: string;
  osVersion: string;
  processor: string;
  ramAmount: string;
  description?: string;
  userId?: number;
}
