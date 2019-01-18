import { environment } from 'src/environments/environment.local';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isGeneratedFile } from '@angular/compiler/src/aot/util';
import { ShareService } from 'src/app/share.service';
import { DOCUMENT } from '@angular/common';
import { Guid } from "guid-typescript";
import { Router } from '@angular/router';

@Component({
  selector: 'history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent {
  


  matches:IGame[];
  
  username: string = "qwe";

  currentMatch: IMatch = {
    game: {
      id:Guid.create(),
      countOfRounds: 0,
      isFinished: false,
      data: new Date(),
    },
    cards:[],
    gamers:[]
  };

  constructor(private http: HttpClient,@Inject(DOCUMENT) private document: any,private router: Router)
  {
   
     this.Gethistory();
    
  }

    Gethistory(){
     
    this.http.get<IGame[]>(environment.apiUrl+'/api/Account/'+this.username).subscribe(result => {
      
      this.matches = result
     }, error => console.error(error)); 
  }

 async GetGameDetails(id:Guid){

   await this.http.delete<IMatch>(environment.apiUrl+'/api/Game/'+id).toPromise().then(result => {
      
      this.currentMatch = result
 
      console.log(this.currentMatch);
     }, error => console.error(error));  
 
    
  }

  CloseDetails(id:Guid){
    this.currentMatch = {
      game: {
        id:Guid.create(),
        countOfRounds: 0,
        isFinished: true,
        data: new Date(),
      },
      cards:[],
      gamers:[]
    };
    
    
  }

  RedirectToMain(s){

    this.router.navigate(['/']);
  
    
  }
  

}

class IMatch {
  game: IGame;
  gamers: IGamer[];
  cards: ICard[];
}

interface IGame {
  id:Guid;
  data: Date;
  countOfRounds: number;
  isFinished: boolean;
}

interface IGamer {
  id: Guid;
  points: number;
  isDealer: boolean;
  isFinished: boolean;
}

interface ICard {
  userInGameID: Guid;
  value: string;
  suit: string;
  raundNumber: number;

}