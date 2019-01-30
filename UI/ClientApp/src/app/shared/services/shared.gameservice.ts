import { HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { IMatch } from 'src/app/shared/interfaces/IMatch';
import { Observable } from 'rxjs';


export class GameService {
  public getGame(http: HttpClient, userName: string): Observable<IMatch> {
    return http.get<IMatch>(environment.apiUrl + '/api/Game/GetLastMatch/'+userName);
  }

  public nextRound(http: HttpClient, userName: string): Observable<IMatch> {
    return http.put<IMatch>(environment.apiUrl + '/api/Game/NextRound/' + userName, 1)
  }

  public stopGame(http: HttpClient, userName: string): Observable<IMatch> {
    return http.put<IMatch>(environment.apiUrl + '/api/Game/NextRound/' + userName, 0)
  }
}