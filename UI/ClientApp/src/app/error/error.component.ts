import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameService } from 'src/app/shared/services/shared.gameservice';

@Component({
  selector: 'error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css'],
  providers: [GameService]
})
export class ErrorComponent {
  constructor(private http: HttpClient, private gameService: GameService) {
  }
}











