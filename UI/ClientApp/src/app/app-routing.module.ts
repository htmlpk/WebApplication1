import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: '', component: AppComponent},
  {path: 'http://localhost:4200/login', component: LoginComponent}

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
