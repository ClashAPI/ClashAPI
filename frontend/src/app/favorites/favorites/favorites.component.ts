import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {
  baseUrl = environment.apiUrl;
  playerFollows?: any = {};
  clanFollows?: any = {};
  deckFollows?: any = {};
  isLoading: boolean;
  constructor(private http: HttpClient, private noty: NotyService, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getFollows();
  }

  getFollows() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'follows')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      if (!data.hasOwnProperty('isFollowingAnyone')) {
        // @ts-ignore
        this.playerFollows = data.playerFollows;
        // @ts-ignore
        this.clanFollows = data.clanFollows;
      }
    }, error => {
      this.noty.error('A követések lekérése sikertelen');
      console.log(error);
    });
  }
}
