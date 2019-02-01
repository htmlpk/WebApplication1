import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginService } from 'src/app/shared/services/shared.loginservice';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService]
})
export class LoginComponent implements OnInit {
  countofbots: string = '1';
  users: string[];
  username: string;
  token: string;
  nomer: number;
  constructor(private http: HttpClient, private router: Router, private loginservice: LoginService) {
  }

  ngOnInit() {
    this.getUsersName();
  }

  public getUsersName(): void {
    this.loginservice.getUsersName(this.http).subscribe(result => {
      this.users = result;
    })
  }

  public login(): void {
    this.loginservice.login(this.http, this.username).subscribe(result => {
      localStorage.setItem('token', JSON.stringify(result));
    })
  }

  public startGame(): void {
    this.loginservice.login(this.http, this.username).subscribe(result => {
      localStorage.setItem('token', JSON.stringify(result));
      this.loginservice.startGame(this.http, this.username, this.countofbots).subscribe(result => {
        this.router.navigate(['/game'])
      });
    });
  }

  public watchHistory(): void {
    this.login();
    this.router.navigate(['/history']);
  }
}





