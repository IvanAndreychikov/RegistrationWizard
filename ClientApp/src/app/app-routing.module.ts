import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './registration/registration.component';
import { HomeComponent } from './home/home.component';
import { RegistrationStep1Component } from './registration/registration-step1/registration-step1.component';
import { RegistrationStep2Component } from './registration/registration-step2/registration-step2.component';
import { RegistrationSuccessComponent } from './registration/success/success.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'register', component: RegistrationComponent,
    children: [
      { path: '', redirectTo: 'step1', pathMatch: 'full' },
      { path: 'step1', component: RegistrationStep1Component },
      { path: 'step2', component: RegistrationStep2Component },
      { path: 'success', component: RegistrationSuccessComponent }
    ]
   },
  { path: '**', redirectTo: 'register' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
