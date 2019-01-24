import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';



export class LoginService {


  public getUsersName(http: HttpClient): Observable<string[]> {
    return http.get<string[]>(environment.apiUrl+'/api/Account/');
  };

  public login(http: HttpClient, username: string): Observable<any> {
    return http.get(environment.apiUrl + '/api/Account/' + username);
  };

  public startGame(http: HttpClient, username: string,countofbots:string): Observable<any> {
    return http.put(environment.apiUrl + '/api/Account/' + username, countofbots);
  }
}