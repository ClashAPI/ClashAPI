import {Component, Inject, Input, OnInit} from '@angular/core';
import {environment} from '../../../environments/environment';
import {FavoritesComponent} from '../favorites/favorites.component';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';

@Component({
  selector: 'app-favorite-clans',
  templateUrl: './favorite-clans.component.html',
  styleUrls: ['./favorite-clans.component.css']
})
export class FavoriteClansComponent implements OnInit {
  @Input() clans?: any;
  baseUrl = environment.apiUrl;
  constructor(@Inject(FavoritesComponent) private parent: FavoritesComponent, private http: HttpClient, private noty: NotyService) { }

  ngOnInit() {
  }
  unfollowClan(clanTag, index) {
    this.http.post(this.baseUrl + 'follows/remove/clan', {id: clanTag}).subscribe(() => {
      this.parent.clanFollows.splice(index, 1);
      this.noty.success();
    }, error => {
      this.noty.error();
    });
  }
}
