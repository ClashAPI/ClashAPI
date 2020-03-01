import {Component, OnInit} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {finalize} from 'rxjs/operators';
import {Router} from '@angular/router';

@Component({
  selector: 'app-admin-health',
  templateUrl: './admin-health.component.html',
  styleUrls: ['./admin-health.component.css']
})
export class AdminHealthComponent implements OnInit {
  baseUrl = environment.apiUrl;
  health: any = {};
  updateDate: any;
  isLoading = false;
  cpuUsage: any;
  ramUsage: any;
  ram: any;
  math = Math;
  isLoaded = false;
  usersToday: any;
  usersCount: number;
  systemMessages: any;

  constructor(private http: HttpClient, private noty: NotyService, private router: Router) { }

  ngOnInit() {
    this.router.navigate(['/']);
    /*
    this.getServerLoad();
    this.getRoyaleapiHealth();
    this.getUsersInfo();
    this.updateData();
    */
  }
  updateData() {
    window.setInterval(() => {
      this.getServerLoad();
      this.getRoyaleapiHealth();
      this.getUsersInfo();
    }, 2000);
  }
  getUsageHealth(percent: number) {
    if (percent < 70) {
      return 'primary';
    } else if (percent < 85) {
      return 'warning';
    } else {
      return 'danger';
    }
  }
  getRoyaleapiHealth() {
    this.http.get(this.baseUrl + 'health/royaleapi').subscribe((data) => {
      this.health = data;
    }, error => {
      this.noty.error('A RoyaleAPI állapotának lekérése sikertelen');
    }, () => {
      this.updateDate = Date.now();
    });
  }
  getServerLoad() {
    if (!this.isLoaded) {
      this.isLoading = true;
    }
    this.http.get(this.baseUrl + 'health/load')
      .pipe(finalize(() => {
        if (!this.isLoaded) {
          this.isLoading = false;
          this.isLoaded = true;
        }
      }))
      .subscribe((data) => {
      // @ts-ignore
      this.cpuUsage = data.cpu;
      // @ts-ignore
      this.ram = data.ramMax;
      // @ts-ignore
      this.ramUsage = data.ram;
    }, (error) => {
      this.noty.error('A szerver terheltségének lekérése sikertelen');
      console.log(error);
    });
  }
  getUsersInfo() {
    this.http.get(this.baseUrl + 'health/users').subscribe((data) => {
      // @ts-ignore
      this.usersToday = data.usersRegisteredToday;
      // @ts-ignore
      this.usersCount = data.users;
    }, () => {
      this.noty.error('Hiba a felhasználói adatok lekérése során');
    });
  }
  runGc() {
    this.http.post(this.baseUrl + 'health/rungc', null).subscribe(() => {
      this.noty.success('A GC sikeresen lefutott');
    }, () => {
      this.noty.error('A GC lefuttatása sikertelen');
    });
  }
}
