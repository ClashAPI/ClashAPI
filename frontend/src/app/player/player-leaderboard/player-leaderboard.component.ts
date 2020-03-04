import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {NgxUiLoaderConfig, NgxUiLoaderService, SPINNER} from 'ngx-ui-loader';
import {environment} from '../../../environments/environment.prod';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-player-leaderboard',
  templateUrl: './player-leaderboard.component.html',
  styleUrls: ['./player-leaderboard.component.css']
})
export class PlayerLeaderboardComponent implements OnInit {
  baseUrl = environment.apiUrl;
  players: any;
  isLoading: boolean;

  constructor(private http: HttpClient, private noty: NotyService, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getPlayerLeaderboard();
  }

  getPlayerLeaderboard() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'players/leaderboard')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      // @ts-ignore
        this.players = data.items;
    }, (error) => {
      this.noty.error('A játékos toplista lekérése sikertelen');
    });
  }

  MathAbs(x) {
    x = +x;
    return (x > 0) ? x : 0 - x;
  }
}
