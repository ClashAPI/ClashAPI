import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {NotyService} from '../../_services/noty.service';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-player-details',
  templateUrl: './player-details.component.html',
  styleUrls: ['./player-details.component.css']
})
export class PlayerDetailsComponent implements OnInit {
  baseUrl = environment.apiUrl;
  isLoading: boolean;
  player: any = {};
  playerChests: any = {};
  challenge12Wins: any;
  grandChallenge12Wins: any;
  draws: any;
  cardsCount: any;

  constructor(private http: HttpClient, private route: ActivatedRoute, private noty: NotyService,
              private router: Router, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getPlayer(this.route.snapshot.paramMap.get('id'));
  }

  getPlayer(id: string) {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'players/' + id + '/chests').subscribe((data) => {
      // @ts-ignore
      this.playerChests = data.model;
    }, (error) => {
      this.noty.error('A játékos ládáinak lekérése sikertelen');
      this.router.navigate(['/']);
    });
    this.http.get(this.baseUrl + 'cards').subscribe((data) => {
      // @ts-ignore
      this.cardsCount = data.items.length;
    }, (error) => {
      this.noty.error('A kártyák lekérése sikertelen');
      this.router.navigate(['/']);
    });
    this.http.get(this.baseUrl + 'players/' + id)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      // @ts-ignore
      this.player = data;
      this.challenge12Wins = this.player.badges.find(o => o.name === 'Classic12Wins') ? this.player.badges.find(o => o.name === 'Classic12Wins').progress : 0;
      this.grandChallenge12Wins = this.player.badges.find(o => o.name === 'Grand12Wins') ? this.player.badges.find(o => o.name === 'Grand12Wins').progress : 0;
      this.draws = this.player.battleCount - (this.player.wins + this.player.losses);
    }, (error) => {
      this.noty.error('A játékos adatainak lekérése sikertelen');
      console.log(error);
      this.router.navigate(['/']);
    });
  }
}
