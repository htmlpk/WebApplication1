import { environment } from 'src/environments/environment.local';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Match } from 'src/app/shared/models/match.model';
import { GameService } from 'src/app/shared/services/game.service';
import { Router } from '@angular/router';
import { GameStatus } from 'src/app/shared/enums/game-status.enum';
import { GamerStatus } from 'src/app/shared/enums/gamer-status.enum';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})
export class GameComponent implements OnInit {

  match: Match;
  cardUrl: string = environment.imageUrl;

  constructor(private http: HttpClient, private gameService: GameService, private router: Router) {
  }

  ngOnInit(): void {
    this.getGame();
  }

  public getGame(): void {
    this.gameService.getGame().subscribe(result => {
      console.log(result);
      this.match = result;
    });
  }

  public nextRound(): void {
    this.gameService.nextRound().subscribe(result => {
      console.log(result);
      this.match = result;
    });
  }

  public stopGame(): void {
    this.gameService.stopGame().subscribe(result => {
      console.log(result);
      this.match = result;
    });
  }

  public isGameFinished(): boolean {
    if (this.match.game.status == GameStatus.Finished) {
      return true;
    }
    return false;
  }

  public isGamerBot(gamerName: string): boolean {
    if (gamerName.indexOf('Bot') != 0) {
      return false;
    }
    return true;
  }

  public isGamerLoser(gamerStatus: GamerStatus): boolean {
    if (gamerStatus == GamerStatus.Loser) {
      return true;
    }
    return false;
  }

  public isGamerWinner(gamerStatus: GamerStatus): boolean {
    if (gamerStatus == GamerStatus.Winner) {
      return true;
    }
    return false;
  }

  public isGamerStillPlay(gamerStatus: GamerStatus): boolean {
    if (gamerStatus == GamerStatus.InGame) {
      return true;
    }
    return false;
  }

  public isCardHolder(userInGameId: Guid, gamerid: Guid): boolean {
    if (userInGameId == gamerid) {
      return true;
    }
    return false;
  }
}