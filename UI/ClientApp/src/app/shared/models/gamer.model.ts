import { Guid } from 'guid-typescript';
import { GamerStatus } from '../enums/gamer-status.enum';

export class Gamer 
{
    id: Guid;
    points: number;
    isDealer: string;
    isFinished: string;
    gamerStatus : GamerStatus
  }