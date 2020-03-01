import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {environment} from '../../../environments/environment';
import {ActivatedRoute, Router} from '@angular/router';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {AuthService} from '../../_services/auth.service';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-clan-details',
  templateUrl: './clan-details.component.html',
  styleUrls: ['./clan-details.component.css']
})
export class ClanDetailsComponent implements OnInit {
  baseUrl = environment.apiUrl;
  dtOptions: DataTables.Settings = {
    language: {
      url: '//cdn.datatables.net/plug-ins/1.10.20/i18n/Hungarian.json'
    },
    paging: false,
    info: false
  };
  clan: any = {};
  isFollowing: any;
  isLoading: boolean;

  constructor(private http: HttpClient, private noty: NotyService, private route: ActivatedRoute,
              private router: Router, private spinner: NgxUiLoaderService, private authService: AuthService) { }

  ngOnInit() {
    const clanId = this.route.snapshot.paramMap.get('id');
    this.queryClan(clanId);
    this.getIsFollowingClan(clanId);
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  queryClan(id: string) {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'clans/' + id)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      // @ts-ignore
      this.clan = data;
    }, error => {
      this.noty.error('A klán adatainak lekérése sikertelen');
      console.log(error);
      this.router.navigate(['/']);
    });
  }
  getIsFollowingClan(id: string) {
    if (this.authService.loggedIn()) {
      this.http.get(this.baseUrl + 'clans/' + id + '/followage').subscribe((data) => {
        // @ts-ignore
        this.isFollowing = data;
      }, error => {
        this.noty.error('A klán követése állapotának lekérése sikertelen');
        this.router.navigate(['/']);
      });
    }
  }
  followClan() {
    this.http.post(this.baseUrl + 'follows/new/clan', {id: this.clan.tag.substr(1), clanName: this.clan.name}).subscribe(() => {
      this.isFollowing = true;
      this.noty.success();
    }, error => {
      this.noty.error();
    });
  }
  unfollowClan() {
    this.http.post(this.baseUrl + 'follows/remove/clan', {id: this.clan.tag.substr(1)}).subscribe(() => {
      this.isFollowing = false;
      this.noty.success();
    }, error => {
      this.noty.error();
    });
  }
}
