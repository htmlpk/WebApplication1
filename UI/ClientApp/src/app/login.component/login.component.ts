import { environment } from 'src/environments/environment.local';
import { Observable } from 'rxjs';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { httpFactory } from '@angular/http/src/http_module';
import {tick,async} from "@angular/core/testing"
import { DOCUMENT } from '@angular/common';
import { ShareService } from 'src/app/share.service';
import { Router } from '@angular/router';



@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  
})
export class LoginComponent {
  
  
  countofbots: string='1';
  users:string[];
  username:string;
  Share:ShareService;
  nomer:number;
  constructor(private http: HttpClient,@Inject(DOCUMENT) private document: any,public share: ShareService,private router: Router) { 

  
    
    
    this.http.get<string[]>(environment.apiUrl+'/api/Account/')
    .subscribe(result => {
      console.log(result);
      this.users = result;
      
    }, error => console.error(error));
    
    
  }  

  login() {
    
    this.http.put<string>(environment.apiUrl+'/api/Account/'+this.username,parseInt(this.countofbots)).subscribe(result => {this.router.navigate(['/game']) 
    }, error => console.error(error));
     
  }

 WatchHistory() {
     
  this.router.navigate(['/history']);
   
}
 
    

}

interface IGame
{
Data:Date;
CountOfRounds:number;
Gamers:IUserInGame[];
IsFinished:string;
}



interface ICard
{
  ID:string ;
  Suit:string  ;
  Value:string  ;
  Points:number  ;
 

}

interface IUserInGame
{
  
  IsDealer:string;
  IsFinished:string;
  Points:number;
  GameStatus:string;

}

