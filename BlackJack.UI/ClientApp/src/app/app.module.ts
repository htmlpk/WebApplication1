
import { LoginComponent } from './login.component/login.component';
import { GameComponent } from './game.component/game.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, InjectionToken } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; 

import { AppComponent } from './app.component';
import { RouterModule,Routes} from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http'

import {APP_BASE_HREF} from '@angular/common'
import {environment} from 'src/environments/environment.local'
import {BASE_URL} from './InjectionToken'
import { ShareService } from './share.service';

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
  
  providers: [ShareService],
  bootstrap: [AppComponent]
})
export class AppModule { }


