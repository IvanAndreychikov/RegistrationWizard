import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { RegistrationService } from '../registration.service';
import { Step1Data } from '../registration-step1/registration-step1.component';

interface Country {
  id: number;
  name: string;
}

interface Province {
  id: number;
  name: string;
  countryId: number;
}

interface Step2Data {
  countryId: number;
  provinceId: number;
}

type RegistrationRequest = Pick<Step1Data, 'login' | 'password'> & Pick<Step2Data, 'countryId' | 'provinceId'>;

@Component({
  selector: 'app-registration-step2',
  templateUrl: './registration-step2.component.html',
  styleUrls: ['./registration-step2.component.scss']
})
export class RegistrationStep2Component implements OnInit {
  registrationForm: FormGroup;
  countries: Country[] = [];
  provinces: Province[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router, private registrationService: RegistrationService) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      countryId: ['', Validators.required],
      provinceId: ['', Validators.required]
    });

    this.loadCountries();
  }

  loadCountries() {
    this.http.get<Country[]>('http://localhost:5251/countries').subscribe(data => {
      this.countries = data;
    });
  }

  onCountryChange(countryId: number) {
    this.http.get<Province[]>(`http://localhost:5251/provinces/${countryId}`).subscribe(data => {
      this.provinces = data;
    });
  }

  onSave() {
    if (this.registrationForm.valid) {
      const {login, password} = this.registrationService.getStep1Data();
      const {countryId, provinceId} = this.registrationForm.value as Step2Data;
      const registrationRequest: RegistrationRequest = { login, password, countryId, provinceId };
      console.log(registrationRequest);

      this.http.post('http://localhost:5251/register', registrationRequest).subscribe({
        next: () => this.router.navigate(['/register/success']),
        error: (err) => alert(`Registration failed: ${err.error ?? err.message}`)
      });
    }
  }
}
