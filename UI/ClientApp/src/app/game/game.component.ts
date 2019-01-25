import { environment } from 'src/environments/environment.local';
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IMatch } from 'src/app/shared/interfaces/IMatch';
import { GameService } from 'src/app/shared/services/shared.gameservice';

@Component({
  selector: 'game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})
export class GameComponent implements OnInit {

  match: IMatch = {
    game: {
      id:null,
      countOfRounds: 0,
      isFinished: null,
      data: new Date(),
    },
    rounds: [],
    gamers: [],
  };

  cardUrl: string = environment.imageUrl;
  username: string = "q";

  constructor(private http: HttpClient, private gameService: GameService) {
  }

  ngOnInit():void {
    this.getGame();
  }

  getGame():void {
    this.gameService.getGame(this.http, this.username).subscribe(result => {  
      this.match = result;      
    }, error => { console.error(error) });
  }

  nextRound():void {
    this.gameService.nextRound(this.http, this.username).subscribe(result => {  
      this.match = result;      
    }, error => { console.error(error) });
  }

  stopGame():void {
    this.gameService.stopGame(this.http, this.username).subscribe(result => {  
      this.match = result;      
    }, error => { console.error(error) });
  }
}











