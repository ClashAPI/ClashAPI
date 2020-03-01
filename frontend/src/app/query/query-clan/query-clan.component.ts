import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-query-clan',
  templateUrl: './query-clan.component.html',
  styleUrls: ['./query-clan.component.css']
})
export class QueryClanComponent implements OnInit {
  model: any = {};

  constructor(private router: Router) { }

  ngOnInit() {
  }

  getClan() {
    this.router.navigate(['clan/' + this.model.tag]);
  }
}
