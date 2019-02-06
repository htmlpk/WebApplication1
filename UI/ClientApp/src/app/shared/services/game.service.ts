import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Match } from 'src/app/shared/models/match.model';
import { environment } from 'src/environments/environment.local';
import { Injectable } from '@angular/core';

@Injectable()
export class GameService {
  constructor(private http: HttpClient) {
  }
  private gameApiUrl = environment.gameApiUrl;
  private apiUrl = environment.apiUrl;

  public getGame(): Observable<Match> {
    return this.http.get<Match>(this.apiUrl + this.gameApiUrl + 'GetLastMatch/');
  }

  public nextRound(): Observable<Match> {
    return this.http.put<Match>(this.apiUrl + this.gameApiUrl + 'NextRound/', 1)
  }

  public stopGame(): Observable<Match> {
    return this.http.put<Match>(this.apiUrl + this.gameApiUrl + 'NextRound/', 0)
  }
}