import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../_services/noty.service';
import {environment} from '../../environments/environment.prod';

@Component({
  selector: 'app-announcements',
  templateUrl: './announcements.component.html',
  styleUrls: ['./announcements.component.css']
})
export class AnnouncementsComponent implements OnInit {
  baseUrl = environment.apiUrl;
  announcements: any;

  constructor(private http: HttpClient, private noty: NotyService) { }

  ngOnInit() {
    this.getAnnouncements();
  }

  getAnnouncements() {
    this.http.get(this.baseUrl + 'announcements').subscribe((data) => {
      this.announcements = data;
    }, (error) => {
      this.noty.error('A közlemények lekérése sikertelen');
    });
  }
}
