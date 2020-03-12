import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-player-arena-name',
  templateUrl: './player-arena-name.component.html',
  styleUrls: ['./player-arena-name.component.css']
})
export class PlayerArenaNameComponent implements OnInit {
  @Input() trophies: number;
  constructor() { }

  ngOnInit() {
  }

}
