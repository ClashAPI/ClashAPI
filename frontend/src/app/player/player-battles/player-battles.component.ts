import {Component, OnInit} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {NotyService} from '../../_services/noty.service';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-player-battles',
  templateUrl: './player-battles.component.html',
  styleUrls: ['./player-battles.component.css']
})
export class PlayerBattlesComponent implements OnInit {
  baseUrl = environment.apiUrl;
  isLoading = false;
  player: any;
  playerBattles: any;

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router,
              private noty: NotyService, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getPlayer(this.route.snapshot.paramMap.get('id'));
    this.getPlayerBattles(this.route.snapshot.paramMap.get('id'));
  }
  getPlayer(id: string) {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'players/' + id).subscribe((data) => {
      // @ts-ignore
      this.player = data;
    }, error => {
      this.noty.error('A játékos adatainak lekérése sikertelen');
      this.router.navigate(['/']);
    });
  }
  getPlayerBattles(id: string) {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'players/' + id + '/battles')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      this.playerBattles = data;
    }, error => {
      this.noty.error('A játékos csatáinak lekérése sikertelen');
      console.log(error);
      this.router.navigate(['/']);
    });
  }
}
