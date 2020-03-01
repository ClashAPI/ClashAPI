import {Component, OnInit, TemplateRef} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {AuthService} from '../../_services/auth.service';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-admin-announcements',
  templateUrl: './admin-announcements.component.html',
  styleUrls: ['./admin-announcements.component.css']
})
export class AdminAnnouncementsComponent implements OnInit {
  dtOptions: DataTables.Settings = {
    language: {
      url: '//cdn.datatables.net/plug-ins/1.10.20/i18n/Hungarian.json'
    },
    paging: true,
    info: true,
  };
  baseUrl = environment.apiUrl;
  isLoading = false;
  announcements: any = {};
  model: any = {};
  modalRef: BsModalRef;
  announcement: any;
  items = [
      'info',
      'warning',
      'danger',
      'primary',
      'secondary'
  ];

  constructor(private http: HttpClient, private noty: NotyService,
              private authService: AuthService, private spinner: NgxUiLoaderService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getAnnouncements();
  }

  getAnnouncements() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'announcements')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
        this.announcements = data;
      }, (error) => {
        this.noty.error('A közlemények lekérése sikertelen');
        console.log(error);
      });
  }
  addAnnouncement() {
    this.isLoading = true;
    this.spinner.start();
    this.http.post(this.baseUrl + 'announcements', this.model)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
        this.announcements.push(data);
        this.noty.success();
      }, (error) => {
        this.noty.error('A közlemény mentése sikertelen');
        console.log(error);
      });
  }
  updateAnnouncement() {
    this.isLoading = true;
    this.spinner.start();
    console.log(this.announcement);
    this.http.put(this.baseUrl + 'announcements/' + this.announcement.id, this.announcement)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe(() => {
        const indexOfPost = this.announcements.filter((localPost) => {
          return localPost.id === this.announcement.id;
        }).index;
        this.announcement[indexOfPost] = this.announcement;
        this.noty.success('A közlemény sikeresen frissítve');
      }, (error) => {
        this.noty.error('A közlemény frissítése sikertelen');
        console.log(error);
      });
  }
  deleteAnnouncement(announcement: any, index: number) {
    if (confirm(`Biztosan törölni szeretnéd a(z) ${announcement.subject} témájú közleményt?`)) {
      this.isLoading = true;
      this.spinner.start();
      this.http.delete(this.baseUrl + 'announcements/' + announcement.id)
        .pipe(finalize(() => {
          this.isLoading = false;
          this.spinner.stop();
        }))
        .subscribe(() => {
          this.announcements.splice(index, 1);
          this.noty.success('A közlemény sikeresen törölve');
        }, (error) => {
          this.noty.error('A közlemény törlése sikertelen');
          console.log(error);
        });
    }
  }
  openModal(template: TemplateRef<any>, announcement: any) {
    this.announcement = announcement;
    this.modalRef = this.modalService.show(template, {class: 'modal-xl', ignoreBackdropClick: true, keyboard: false});
  }
}
