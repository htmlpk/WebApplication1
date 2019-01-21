import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { IMatch } from 'src/app/shared/interfaces/IMatch';
import { Observable } from 'rxjs';
import { isObservable } from '@angular/core/src/util/lang';
export class GameService {


  public getGame(http: HttpClient, userName: string): Observable<IMatch> {
    return http.get<IMatch>(environment.apiUrl + '/api/Game/' + userName);
  }

  public nextRound(http: HttpClient, userName: string): Observable<IMatch> {
    return http.put<IMatch>(environment.apiUrl + '/api/Game/' + userName, 1)
  }

  public stopGame(http: HttpClient, userName: string): Observable<IMatch> {
    return http.put<IMatch>(environment.apiUrl + '/api/Game/' + userName, 0)
  }

}