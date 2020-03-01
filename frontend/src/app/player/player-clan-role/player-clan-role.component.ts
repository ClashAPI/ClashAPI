import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-player-clan-role',
  templateUrl: './player-clan-role.component.html',
  styleUrls: ['./player-clan-role.component.css']
})
export class PlayerClanRoleComponent implements OnInit {
  @Input() playerClanRole: string;

  constructor() { }

  ngOnInit() {
  }

}
