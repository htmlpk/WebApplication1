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
    }, error => {console.error(error); this.router.navigate(['/error']);});
  }

  public login(): void {
    this.loginservice.login(this.http, this.username).subscribe(result => {
      localStorage.setItem('token', this.token);
      this.token = result;
    }, error => {console.error(error); this.router.navigate(['/error']);});
  }

  public startGame(): void {
    this.loginservice.login(this.http, this.username).subscribe(result => {
      this.token = result;
      localStorage.setItem('token', this.token);
      console.log(localStorage.getItem('token'));
    }, error => console.error(error));
    this.loginservice.startGame(this.http, this.username, this.countofbots).subscribe(result => {
      this.router.navigate(['/game'])
    }, error => {console.error(error); this.router.navigate(['/error']);});;
  }

  public watchHistory(): void {
    this.loginservice.login(this.http, this.username).subscribe(result => {
      this.token = result;
      localStorage.setItem('token', this.token);
      this.router.navigate(['/history']);
    }, error => {console.error(error); this.router.navigate(['/error']);});
  }
}





