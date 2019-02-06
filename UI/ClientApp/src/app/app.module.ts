import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { ErrorComponent } from 'src/app/error/error.component';
import { GameComponent } from 'src/app/game/game.component';
import { LoginComponent } from 'src/app/login/login.component';
import { GameService } from 'src/app/shared/services/game.service';
import { ParamInterceptor } from 'src/app/shared/services/param.interceptor';

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
    AppRoutingModule
  ],
  providers: [RouterModule,
    GameService, {
    provide: HTTP_INTERCEPTORS,
    useClass: ParamInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }