import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/shared/services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService]
})
export class LoginComponent implements OnInit {
  countOfBots: string = '1';
  users: string[];
  userName: string;
  token: string;
  constructor(private router: Router, private loginService: LoginService) {
  }

  ngOnInit() {
    this.getUsersName();
  }

  public getUsersName(): void {
    this.loginService.getUsersName().subscribe(result => {
      this.users = result;
    })
  }

  public login(): void {
    this.loginService.login(this.userName).subscribe(result => {
      localStorage.setItem('token', JSON.stringify(result));
    })
  }

  public startGame(): void {
    this.loginService.login(this.userName).subscribe(result => {
      localStorage.setItem('token', JSON.stringify(result));
      this.loginService.startGame(this.userName, this.countOfBots).subscribe(result => {
        this.router.navigate(['/game'])
      });
    });
  }

  public watchHistory(): void {
    this.loginService.login(this.userName).subscribe(result => {
      localStorage.setItem('token', JSON.stringify(result));
      this.router.navigate(['/history']);
    })
  }

  buttonState() {
    if (this.userName){
      console.log(this.userName)
      let val = this.userName.match(/.*Bot.*/);
      console.log(val)
      return (this.userName== '' || val) ? true : false;
    }
    return
  }
}