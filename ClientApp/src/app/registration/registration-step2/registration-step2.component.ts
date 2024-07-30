import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Country, Province, RegistrationRequest, RegistrationService, Step2Data } from '../registration.service';

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
    this.registrationService.getCountries().subscribe(data => {
      this.countries = data;
    });
  }

  onCountryChange(countryId: number) {
    this.registrationService.getProvinces(countryId).subscribe(data => {
      this.provinces = data;
    });
  }

  onSave() {
    if (this.registrationForm.valid) {
      const { login, password } = this.registrationService.getStep1Data();
      const { countryId, provinceId } = this.registrationForm.value as Step2Data;
      const registrationRequest: RegistrationRequest = { login, password, countryId, provinceId };

      this.registrationService.register(registrationRequest).subscribe({
        next: () => this.router.navigate(['/register/success']),
        error: (err) => alert(`Registration failed: ${err.error ?? err.message}`)
      });
    }
  }
}
