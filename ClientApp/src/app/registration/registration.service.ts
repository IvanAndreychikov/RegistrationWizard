import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Step1Data {
  login: string;
  password: string;
  confirmPassword: number;
  agree: boolean;
}

export interface Country {
  id: number;
  name: string;
}

export interface Province {
  id: number;
  name: string;
  countryId: number;
}

export interface Step2Data {
  countryId: number;
  provinceId: number;
}

export type RegistrationRequest = Pick<Step1Data, 'login' | 'password'> & Pick<Step2Data, 'countryId' | 'provinceId'>;

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private step1Data: Step1Data;
  private baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.baseUrl}/countries`);
  }

  getProvinces(countryId: number): Observable<Province[]> {
    return this.http.get<Province[]>(`${this.baseUrl}/provinces/${countryId}`);
  }

  register(data: RegistrationRequest): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/register`, data);
  }

  setStep1Data(data: Step1Data) {
    this.step1Data = data;
  }

  getStep1Data() {
    return this.step1Data;
  }
}
