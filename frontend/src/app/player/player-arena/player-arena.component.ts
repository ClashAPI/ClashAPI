import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-player-arena',
  templateUrl: './player-arena.component.html',
  styleUrls: ['./player-arena.component.css']
})
export class PlayerArenaComponent implements OnInit {
  @Input() player: any;
  baseUrl = 'http://localhost:4200/';

  constructor() { }

  ngOnInit() {
  }

}
