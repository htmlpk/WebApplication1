import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from "guid-typescript";
import { Game } from 'src/app/shared/models/game.model';
import { Match } from 'src/app/shared/models/match.model';
import { HistoryService } from 'src/app/shared/services/history.service';

@Component({
  selector: 'history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css'],
  providers: [HistoryService]
})
export class HistoryComponent {
  matches: Game[];
  currentMatch: Match = {
    game: {
      id: Guid.create(),
      countOfRounds: 0,
      status: 0,
      date: new Date(),
    },
    rounds: [],
    gamers: []
  };

  constructor(private http: HttpClient, private router: Router, private historyService: HistoryService) {
    this.getHistory();
  }

  getHistory(): void {
    this.historyService.gethistory().subscribe(result => {
      this.matches = result
    });
  }

  getGameDetails(matchId: Guid): void {
    this.historyService.getGameDetails(matchId).subscribe(result => {
      this.currentMatch = result
      console.log(result);
    });
  }

  closeDetails(): void {
    this.currentMatch = {
      game: {
        id: Guid.create(),
        countOfRounds: 0,
        status: 0,
        date: new Date(),
      },
      rounds: [],
      gamers: []
    };
  }

  redirectToMain() {
    this.router.navigate(['/']);
  }
}