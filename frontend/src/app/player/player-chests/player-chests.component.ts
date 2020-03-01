import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-player-chests',
  templateUrl: './player-chests.component.html',
  styleUrls: ['./player-chests.component.css']
})
export class PlayerChestsComponent implements OnInit {
  @Input() playerChests: any;

  constructor() { }

  ngOnInit() {
  }

}
