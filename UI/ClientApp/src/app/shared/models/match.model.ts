import { Game } from 'src/app/shared/models/game.model';
import { Gamer } from 'src/app/shared/models/gamer.model';
import { Round } from 'src/app/shared/models/round.model';

export class Match {
    game: Game;
    gamers: Gamer[];
    rounds: Round[];
  }