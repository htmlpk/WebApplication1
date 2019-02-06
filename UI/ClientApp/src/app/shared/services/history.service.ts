import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { Match } from 'src/app/shared/models/match.model';
import { Game} from 'src/app/shared/models/game.model';
import { Observable } from 'rxjs';
import { Guid } from "guid-typescript";
import { Injectable } from '@angular/core';

@Injectable()
export class HistoryService {
  constructor(private http: HttpClient) {
  }
  private historyApiUrl = environment.historyApiUrl;
  private apiUrl = environment.apiUrl;

  public gethistory(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl + this.historyApiUrl + 'GetAllGames')
  };

  public getGameDetails(id: Guid): Observable<Match> {
    return this.http.get<Match>(this.apiUrl + this.historyApiUrl + 'GetMatchById/'+id);
  };
}