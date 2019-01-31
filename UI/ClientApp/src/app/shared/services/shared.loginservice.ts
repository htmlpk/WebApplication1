import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { StartGameModel } from 'src/app/shared/models/StartGameModel';

export class LoginService {

  public getUsersName(http: HttpClient): Observable<string[]> {
    return http.get<string[]>(environment.apiUrl+'/api/Account/GetUserNames');
  };

  public login(http: HttpClient, username: string): Observable<any> {
    return http.get(environment.apiUrl + '/api/Account/LogIn/' + username);
  };

  public startGame(http: HttpClient, username: string,countofbots:string): Observable<any> {
    let model = new StartGameModel();
    model.userName = username;
    model.countOfBots = parseInt(countofbots);
    return http.post(environment.apiUrl + '/api/Game/StartGame/', model);
  }
}
