import { environment } from 'src/environments/environment.local';
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isGeneratedFile } from '@angular/compiler/src/aot/util';
import { ShareService } from 'src/app/share.service';
import { DOCUMENT } from '@angular/common';
import { Guid } from 'guid-typescript';


import { Observable } from "rxjs";





@Component({
  selector: 'game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {

  match: IMatch = {
    game: {
      countOfRounds: 0,
      isFinished: "",
      data: new Date(),
    },
    cards:[],
    gamers:[]
  };

  cardUrl: string = environment.imageUrl;
  username: string = "q";

  constructor(private http: HttpClient, @Inject(DOCUMENT) private document: any) {
    
    this.GetGame();

    
  }




  async GetGame() {
    await this.http.get<IMatch>(environment.apiUrl + '/api/Game/' + this.username)
      .toPromise()
      .catch()
      .then(result => {
        
        this.match = result;
        console.log(result);

      }, error => { console.error(error) });
  }

  async NextRound() {
    await this.http.put<IMatch>(environment.apiUrl + '/api/Game/' + this.username, 1).toPromise()
    .catch()
    .then(result => {
      
      this.match = result;
      console.log(result);

    }, error => { console.error(error) });
}

  StopGame() {
    this.http.put<IMatch>(environment.apiUrl + '/api/Game/' + this.username, 0).subscribe(result => {
      this.GetGame();
      this.match = result;
    }, error => console.error(error));
    this.GetGame();

  }



}

class IMatch {
  game: IGame;
  gamers: IGamer[];
  cards: ICard[];
}

interface IGame {
  data: Date;
  countOfRounds: number;
  isFinished: string;
}

interface IGamer {
  id: Guid;
  points: number;
  isDealer: string;
  isFinished: string;
}

interface ICard {
  userInGameID: Guid;
  value: string;
  suit: string;
  raundNumber: number;

}



