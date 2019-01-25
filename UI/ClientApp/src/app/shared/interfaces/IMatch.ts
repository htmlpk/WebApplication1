import {Round} from '../classes/Round';
import {Gamer} from '../classes/Gamer';
import {Game} from '../classes/Game';


export interface IMatch {
    game: Game;
    gamers: Gamer[];
    rounds: Round[];
  }