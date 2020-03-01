import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../_services/noty.service';
import {environment} from '../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  baseUrl = environment.apiUrl;
  health: any = {};
  updateDate: any;
  isLoading = false;
  posts: any = {};

  constructor(private http: HttpClient, private noty: NotyService, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getPosts();
  }
  getPosts() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'posts')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      this.posts = data;
      this.posts = this.posts.filter((post) => post.isVisible);
    }, (error) => {
      this.noty.error('A cikkek lekérése sikertelen');
      console.log(error);
    });
  }
}
