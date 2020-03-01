import {Component, OnInit, TemplateRef} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {environment} from '../../../environments/environment';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {AuthService} from '../../_services/auth.service';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-admin-cr-accounts',
  templateUrl: './admin-cr-accounts.component.html',
  styleUrls: ['./admin-cr-accounts.component.css']
})
export class AdminCrAccountsComponent implements OnInit {
  dtOptions: DataTables.Settings = {
    language: {
      url: '//cdn.datatables.net/plug-ins/1.10.20/i18n/Hungarian.json'
    },
    paging: true,
    info: true,
  };
  baseUrl = environment.apiUrl;
  isLoading: boolean;
  modalRef: BsModalRef;
  accounts: any;
  account: any;

  constructor(private http: HttpClient, private noty: NotyService,
              private spinner: NgxUiLoaderService, private modalService: BsModalService,
              private authService: AuthService) { }

  ngOnInit() {
    this.getAccounts();
  }

  openModal(template: TemplateRef<any>, account: any) {
    this.account = account;
    this.modalRef = this.modalService.show(template);
  }
  getAccounts() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'users/accounts')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      this.accounts = data;
    }, (error) => {
      this.noty.error('A fiókok lekérése sikertelen');
      console.log(error);
    });
  }
  verifyAccount(account: any) {
    console.log(account);
    this.http.post(this.baseUrl + 'users/' + account.userId + '/accounts/' + account.playerId + '/verify', {})
      .subscribe(() => {
        this.accounts.find(a => a.playerId === account.playerId).isVerified = true;
        this.noty.success('A fiók sikeresen megerősítve');
      }, () => {
        this.noty.error();
      });
  }
  deleteAccount(account: any, index: number) {
    this.isLoading = true;
    this.spinner.start();
    this.http.delete(this.baseUrl + 'users/' + account.userId + '/accounts/' + account.playerId)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe(() => {
      this.accounts.splice(index, 1);
      this.noty.success();
    }, (error) => {
      this.noty.error('A fiók törlése sikertelen');
      console.log(error);
    });
  }
}
