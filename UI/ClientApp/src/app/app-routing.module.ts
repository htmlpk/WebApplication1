import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from 'src/app/error/error.component';
import { GameComponent } from 'src/app/game/game.component';
import { LoginComponent } from 'src/app/login/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'game', component: GameComponent },
  { path: 'history', loadChildren: './history/history.module#HistoryModule' },
  { path: 'error', component: ErrorComponent }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }