import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.local';
import { IMatch } from 'src/app/shared/interfaces/IMatch';
import { Game} from 'src/app/shared/classes/Game';

import { isObservable } from '@angular/core/src/util/lang';
import { Guid } from "guid-typescript";

export class LoginService {


  public getUsersName(http: HttpClient): Observable<string[]> {
    return http.get<string[]>(environment.apiUrl+'/api/Account/');
  };

  public login(http: HttpClient, currentUserName: string,countofbots:string): Observable<any> {
    return http.put(environment.apiUrl + '/api/Account/' + currentUserName, parseInt(countofbots));
  };
}