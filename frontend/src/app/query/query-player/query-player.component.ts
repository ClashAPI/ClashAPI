import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-query-player',
  templateUrl: './query-player.component.html',
  styleUrls: ['./query-player.component.css']
})
export class QueryPlayerComponent implements OnInit {
  model: any = {};

  constructor(private router: Router) { }

  ngOnInit() {
  }

  getPlayer() {
    this.router.navigate(['player/' + this.model.tag]);
  }
}
