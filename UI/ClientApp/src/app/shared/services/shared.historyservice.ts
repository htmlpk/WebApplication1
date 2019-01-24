import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { IMatch } from 'src/app/shared/interfaces/IMatch';
import { Game} from 'src/app/shared/classes/Game';
import { Observable } from 'rxjs';
import { Guid } from "guid-typescript";

export class HistoryService {


  public gethistory(http: HttpClient, userName: string): Observable<Game[]> {
    return http.get<Game[]>(environment.apiUrl+'/api/History/')
  };

  public getGameDetails(http: HttpClient, id: Guid): Observable<IMatch> {
    return http.get<IMatch>(environment.apiUrl+'/api/History/'+id);
  };
}