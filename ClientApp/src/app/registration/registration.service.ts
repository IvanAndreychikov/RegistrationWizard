import { Injectable } from '@angular/core';
import { Step1Data } from './registration-step1/registration-step1.component';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private step1Data: Step1Data;

  setStep1Data(data: Step1Data) {
    this.step1Data = data;
  }

  getStep1Data() {
    return this.step1Data;
  }
}
