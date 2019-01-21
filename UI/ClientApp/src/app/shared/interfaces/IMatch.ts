import {Card} from '../classes/Card';
import {Gamer} from '../classes/Gamer';
import {Game} from '../classes/Game';


export interface IMatch {
    game: Game;
    gamers: Gamer[];
    cards: Card[];
  }