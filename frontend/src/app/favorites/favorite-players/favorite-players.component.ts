import {Component, Inject, Input, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {environment} from '../../../environments/environment';
import {FavoritesComponent} from '../favorites/favorites.component';

@Component({
  selector: 'app-favorite-players',
  templateUrl: './favorite-players.component.html',
  styleUrls: ['./favorite-players.component.css']
})
export class FavoritePlayersComponent implements OnInit {
  @Input() players?: any;
  baseUrl = environment.apiUrl;
  constructor(@Inject(FavoritesComponent) private parent: FavoritesComponent, private http: HttpClient, private noty: NotyService) { }

  ngOnInit() {
  }
  unfollowPlayer(playerTag, index) {
    this.http.post(this.baseUrl + 'follows/remove/player', {id: playerTag}).subscribe(() => {
      this.parent.playerFollows.splice(index, 1);
      this.noty.success();
    }, error => {
      this.noty.error();
    });
  }
}
