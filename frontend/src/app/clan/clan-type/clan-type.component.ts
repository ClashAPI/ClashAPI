import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-clan-type',
  templateUrl: './clan-type.component.html',
  styleUrls: ['./clan-type.component.css']
})
export class ClanTypeComponent implements OnInit {
  @Input() clanType: string;

  constructor() { }

  ngOnInit() {
  }

}
