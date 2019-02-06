import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameService } from 'src/app/shared/services/game.service';

@Component({
  selector: 'error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css'],
  providers: [GameService]
})
export class ErrorComponent implements OnInit {
  public errormessage:string = "";
  constructor(private http: HttpClient, private gameService: GameService) {
  }
  
  ngOnInit():void {
    this.errormessage = localStorage.getItem('error');
  }
}