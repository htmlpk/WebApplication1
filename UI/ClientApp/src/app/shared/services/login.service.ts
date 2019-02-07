import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { StartGameModel } from 'src/app/shared/models/start-game.model';
import { Injectable } from '@angular/core';

@Injectable()
export class LoginService {
  constructor(private http: HttpClient) {
  }
  private apiUrl = environment.apiUrl;
  private accountApiUrl = '/api/Account/';
  private gameApiUrl = '/api/Game/';

  public getUsersName(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl + this.accountApiUrl + 'GetUserNames');
  };

  public login(userName: string): Observable<any> {
    return this.http.get(this.apiUrl + this.accountApiUrl + 'LogIn/' + userName);
  };

  public startGame(userName: string,countOfBots:string): Observable<any> {
    let model = new StartGameModel();
    model.userName = userName;
    model.countOfBots = parseInt(countOfBots);
    return this.http.post(this.apiUrl + this.gameApiUrl + 'StartGame/', model);
  }
}