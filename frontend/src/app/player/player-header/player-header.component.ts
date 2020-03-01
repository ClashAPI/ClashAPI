import {Component, Input, OnInit} from '@angular/core';
import {AuthService} from '../../_services/auth.service';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';

@Component({
  selector: 'app-player-header',
  templateUrl: './player-header.component.html',
  styleUrls: ['./player-header.component.css']
})
export class PlayerHeaderComponent implements OnInit {
  @Input() player: any;
  isFollowing: any;
  baseUrl = environment.apiUrl;
  badges: any;
  isLoading = false;

  constructor(private authService: AuthService, private http: HttpClient,
              private noty: NotyService, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getIsFollowingPlayer();
    this.getBadges();
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
  getIsFollowingPlayer() {
    if (this.authService.loggedIn()) {
      this.http.get(this.baseUrl + 'players/' + this.player.tag.substr(1) + '/followage').subscribe((data) => {
        this.isFollowing = data;
      }, () => {
        this.noty.error('A játékos követése állapotának lekérése sikertelen');
      });
    }
  }
  getBadges() {
    this.http.get(this.baseUrl + 'players/' + this.player.tag.substr(1) + '/badges').subscribe((data) => {
      this.badges = data;
    }, () => {
      this.noty.error('A játékos jelvényeinek lekérése sikertelen');
    });
  }
  followPlayer() {
    this.http.post(this.baseUrl + 'follows/new/player', {id: this.player.tag.substr(1), playerName: this.player.name}).subscribe(() => {
      this.isFollowing = true;
      this.noty.success();
    }, () => {
      this.noty.error();
    });
  }
  unfollowPlayer() {
    this.http.post(this.baseUrl + 'follows/remove/player', {id: this.player.tag.substr(1)}).subscribe(() => {
      this.isFollowing = false;
      this.noty.success();
    }, () => {
      this.noty.error();
    });
  }
}
