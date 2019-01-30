import { LoginComponent } from './login/login.component';
import { ErrorComponent } from './error/error.component';
import { GameComponent } from './game/game.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { RouterModule,Routes} from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http'
import { ParamInterceptor } from './shared/services/shared.interceptor';
import { GameService } from 'src/app/shared/services/shared.gameservice';

const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'game', component: GameComponent },
  { path: 'history', loadChildren: './history/history.module#HistoryModule' },
  { path: 'error', component: ErrorComponent }
  ];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    GameComponent,
    ErrorComponent
    
  ],
  imports: [
    FormsModule,
    HttpModule,
    HttpClientModule,
    BrowserModule,
    RouterModule.forRoot(routes)
        
  ],
  providers: [GameService, {
    provide: HTTP_INTERCEPTORS,
    useClass: ParamInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }


