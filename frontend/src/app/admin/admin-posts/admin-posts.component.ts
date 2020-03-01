import {Component, OnInit, TemplateRef} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {AuthService} from '../../_services/auth.service';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-admin-posts',
  templateUrl: './admin-posts.component.html',
  styleUrls: ['./admin-posts.component.css']
})
export class AdminPostsComponent implements OnInit {
  public editor = ClassicEditor;
  dtOptions: DataTables.Settings = {
    language: {
      url: '//cdn.datatables.net/plug-ins/1.10.20/i18n/Hungarian.json'
    },
    paging: true,
    info: true,
  };
  baseUrl = environment.apiUrl;
  isLoading = false;
  posts: any = {};
  model: any = {};
  constructor(private http: HttpClient, private noty: NotyService,
              private authService: AuthService, private spinner: NgxUiLoaderService,
              private modalService: BsModalService) { }
  modalRef: BsModalRef;
  post: any;

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
    }, (error) => {
      this.noty.error('A cikkek lekérése sikertelen');
      console.log(error);
    });
  }
  addPost() {
    this.isLoading = true;
    this.spinner.start();
    this.http.post(this.baseUrl + 'posts', this.model)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      this.posts.push(data);
      this.noty.success();
    }, (error) => {
      this.noty.error('A bejegyzés mentése sikertelen');
      console.log(error);
    });
  }
  updatePost() {
    this.isLoading = true;
    this.spinner.start();
    this.http.put(this.baseUrl + 'posts/' + this.post.id, this.post)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe(() => {
      const indexOfPost = this.posts.filter((localPost) => {
        return localPost.id === this.post.id;
      }).index;
      this.posts[indexOfPost] = this.post;
      this.noty.success('A bejegyzés sikeresen frissítve');
    }, (error) => {
      this.noty.error('A bejegyzés frissítése sikertelen');
      console.log(error);
    });
  }
  deletePost(post: any, index: number) {
    if (confirm(`Biztosan törölni szeretnéd a(z) ${post.title} című cikket?`)) {
      this.isLoading = true;
      this.spinner.start();
      this.http.delete(this.baseUrl + 'posts/' + post.id)
        .pipe(finalize(() => {
          this.isLoading = false;
          this.spinner.stop();
        }))
        .subscribe(() => {
        this.posts.splice(index, 1);
        this.noty.success('A cikk sikeresen törölve');
      }, (error) => {
        this.noty.error('A cikk törlése sikertelen');
        console.log(error);
      });
    }
  }
  openModal(template: TemplateRef<any>, post: any) {
    this.post = post;
    this.modalRef = this.modalService.show(template, {class: 'modal-xl', ignoreBackdropClick: true, keyboard: false});
  }
}
