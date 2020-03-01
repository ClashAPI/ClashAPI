import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-clan-badge',
  templateUrl: './clan-badge.component.html',
  styleUrls: ['./clan-badge.component.css']
})
export class ClanBadgeComponent implements OnInit {
  @Input() clan: any;
  constructor() { }

  ngOnInit() {
  }

}
