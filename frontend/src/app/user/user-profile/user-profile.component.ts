import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../_services/auth.service';
import {NotyService} from '../../_services/noty.service';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  baseUrl = environment.apiUrl;
  model: any = {};
  user: any;
  isLoading: boolean;
  currentUserName: any;
  currentEmail: any;

  constructor(private http: HttpClient, private authService: AuthService, private noty: NotyService,
              private router: Router, private route: ActivatedRoute, private ngxService: NgxUiLoaderService) { }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    this.isLoading = true;
    this.ngxService.start();
    this.http.get(this.baseUrl + 'users/' + this.authService.decodedToken.nameid)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.ngxService.stop();
      }))
      .subscribe((data) => {
      this.user = data;
      this.currentUserName = this.authService.decodedToken.unique_name;
      this.currentEmail = this.authService.decodedToken.email;
    }, (error) => {
      this.noty.error('Hiba történt az adataid lekérése során');
      console.log(error);
    });
  }

  saveUser() {
    this.isLoading = true;
    this.ngxService.start();
    this.http.put(this.baseUrl + 'users/' + this.authService.decodedToken.nameid, this.model)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.ngxService.stop();
      }))
      .subscribe(() => {
      this.noty.success();
    }, (error) => {
      this.noty.error();
      console.log(error);
    });
  }
}
