import {EventEmitter} from '@angular/core';

export class ShareService {
  
  public userName:number=100;
  onClick:EventEmitter<number> = new EventEmitter();
 
  public doClick(username:number){
    this.userName = username;
    debugger;
    this.onClick.emit(this.userName);
  }

}