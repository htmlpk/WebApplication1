import { environment } from 'src/environments/environment.local';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Match } from 'src/app/shared/models/match.model';
import { GameService } from 'src/app/shared/services/game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})
export class GameComponent implements OnInit {

  match: Match = {
    game: {
      id: null,
      countOfRounds: 0,
      status: 0,
      date: new Date(),
    },
    rounds: [],
    gamers: [],
  };
  cardUrl: string = environment.imageUrl;
  username: string = "q";

  constructor(private http: HttpClient, private gameService: GameService, private router: Router) {
  }

  ngOnInit(): void {
    this.getGame();
  }

  getGame(): void {
    this.gameService.getGame().subscribe(result => {
      this.match = result;
    });
  }

  nextRound(): void {
    this.gameService.nextRound().subscribe(result => {
      this.match = result;
    });
  }

  stopGame(): void {
    this.gameService.stopGame().subscribe(result => {
      this.match = result;
    });
  }
}