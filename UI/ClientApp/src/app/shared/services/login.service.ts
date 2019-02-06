import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { StartGameModel } from 'src/app/shared/models/start-game.model';
import { Injectable } from '@angular/core';

@Injectable()
export class LoginService {
  constructor(private http: HttpClient) {
  }
  private accountApiUrl = environment.accountApiUrl;
  private gameApiUrl = environment.gameApiUrl;
  private apiUrl = environment.apiUrl;

  public getUsersName(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl + this.accountApiUrl + 'GetUserNames');
  };

  public login(username: string): Observable<any> {
    return this.http.get(this.apiUrl + this.accountApiUrl + 'LogIn/' + username);
  };

  public startGame(username: string,countofbots:string): Observable<any> {
    let model = new StartGameModel();
    model.userName = username;
    model.countOfBots = parseInt(countofbots);
    return this.http.post(this.apiUrl + this.gameApiUrl + 'StartGame/', model);
  }
}
