import { Guid } from 'guid-typescript';
import { GameStatus } from 'src/app/shared/enums/game-status.enum'

export class Game {
  id: Guid;
  date: Date;
  countOfRounds: number;
  status: GameStatus;
}