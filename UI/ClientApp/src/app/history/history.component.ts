import { environment } from 'src/environments/environment.local';
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Game } from 'src/app/shared/classes/Game';
import { IMatch } from 'src/app/shared/interfaces/IMatch';
import { HistoryService } from 'src/app/shared/services/shared.historyservice';
import { DOCUMENT } from '@angular/common';
import { Guid } from "guid-typescript";
import { Router } from '@angular/router';

@Component({
  selector: 'history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css'],
  providers: [HistoryService]
})
export class HistoryComponent {
  matches: Game[];
  currentMatch: IMatch = {
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
    this.historyService.gethistory(this.http, "").subscribe(result => {
      this.matches = result
    }, error => console.error(error));
  }

  getGameDetails(id: Guid): void {
    this.historyService.getGameDetails(this.http, id).subscribe(result => {
      this.currentMatch = result
      console.log(result);
    }, error => console.error(error));
  }

  closeDetails(id: Guid): void {
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

