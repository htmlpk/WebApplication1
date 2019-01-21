
import { LoginComponent } from './login/login.component';
import { GameComponent } from './game/game.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; 
import { AppComponent } from './app.component';
import { RouterModule,Routes} from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http'


const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'game', component: GameComponent },
  { path: 'history', loadChildren: './history/history.module#HistoryModule' }
  ];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    GameComponent,
    
  ],
  imports: [
    FormsModule,
    HttpModule,
    HttpClientModule,
    BrowserModule,
    RouterModule.forRoot(routes)
        
  ],
  
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }


