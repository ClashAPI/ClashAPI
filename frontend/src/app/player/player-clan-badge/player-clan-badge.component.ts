import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-player-clan-badge',
  templateUrl: './player-clan-badge.component.html',
  styleUrls: ['./player-clan-badge.component.css']
})
export class PlayerClanBadgeComponent implements OnInit {
  @Input() player: any;
  constructor() { }

  ngOnInit() {
  }

}
