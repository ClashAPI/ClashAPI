import {Component, OnInit, TemplateRef} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from 'src/app/_services/noty.service';
import {Router} from '@angular/router';
import {AuthService} from 'src/app/_services/auth.service';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {
  dtOptions: DataTables.Settings = {
    language: {
      url: '//cdn.datatables.net/plug-ins/1.10.20/i18n/Hungarian.json'
    },
    paging: true,
    info: true,
  };
  baseUrl = environment.apiUrl;
  users: any = {};
  isLoading: boolean;
  modalRef: BsModalRef;
  user: any;
  userRoles: any;
  selectedTab: any = 'details';

  constructor(private http: HttpClient, private noty: NotyService, private router: Router, private authService: AuthService,
              private spinner: NgxUiLoaderService, private modalService: BsModalService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'users')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      this.users = data;
    }, (error) => {
      this.noty.error('A felhasználók lekérése sikertelen');
      console.log(error);
    });
  }
  getUserRoles(userId: any) {
    this.http.get(this.baseUrl + 'users/' + userId + '/roles')
      .subscribe((data) => {
        this.userRoles = data;
      }, (error) => {
        this.noty.error('A felhasználó csoportjainak lekérése sikertelen');
        console.log(error);
      });
  }
  updateUser() {
    this.isLoading = true;
    this.spinner.start();
    this.http.put(this.baseUrl + 'users/' + this.user.id, this.user)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe(() => {
      const indexOfUser = this.users.filter((localUser) => {
        return localUser.id === this.user.id;
      }).index;
      this.users[indexOfUser] = this.user;
      this.noty.success('A felhasználó sikeresen frissítve');
    }, (error) => {
      this.noty.error('A felhasználó frissítése sikertelen');
      console.log(error);
    });
  }
  deleteUser(user: any, index: number) {
    if (confirm(`Biztosan törölni szeretnéd ${user.username} felhasználót?`)) {
      this.http.delete(this.baseUrl + 'users/' + user.id).subscribe(() => {
        this.users.splice(index, 1);
        this.noty.success('A felhasználó törlése sikeres');
      }, () => {
        this.noty.error('A felhasználó törlése sikertelen');
      });
    }
  }
  openModal(template: TemplateRef<any>, user: any) {
    this.user = user;
    this.modalRef = this.modalService.show(template, {class: 'modal-xl', ignoreBackdropClick: true, keyboard: false});
  }
  onSelect(name: string) {
    this.selectedTab = name;
  }
}
