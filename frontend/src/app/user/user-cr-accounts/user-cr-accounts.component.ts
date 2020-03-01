import {Component, OnInit, TemplateRef} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {NotyService} from '../../_services/noty.service';
import {AuthService} from '../../_services/auth.service';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import {BsModalRef, BsModalService} from 'ngx-bootstrap';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-user-cr-accounts',
  templateUrl: './user-cr-accounts.component.html',
  styleUrls: ['./user-cr-accounts.component.css']
})
export class UserCrAccountsComponent implements OnInit {
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
  accounts: any = {};
  model: any = {};
  modalRef: BsModalRef;
  account: any;

  constructor(private http: HttpClient, private noty: NotyService, private authService: AuthService,
              private spinner: NgxUiLoaderService, private modalService: BsModalService) { }

  ngOnInit() {
    this.getAccounts();
  }

  getAccounts() {
    this.isLoading = true;
    this.spinner.start();
    this.http.get(this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/accounts')
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe((data) => {
      this.accounts = data;
    }, (error) => {
      this.noty.error('A fiókok lekérése sikertelen');
    });
  }

  addAccount() {
    // if (this.model.playerId.test('^[0289CcGgJjLlPpQqRrUuVvYy]*$')) {
      this.isLoading = true;
      this.spinner.start();
      // @ts-ignore
      this.http.post(this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/accounts/' + this.model.playerId)
        .pipe(finalize(() => {
          this.isLoading = false;
          this.spinner.stop();
        }))
        .subscribe((data) => {
          // @ts-ignore
          this.accounts.push(data);
          this.noty.success('Fiók sikeresen hozzáadva');
        }, (error) => {
          if (error.exists) {
            this.noty.error('Ennek a fióknak már igazolták a tulajdonjogát');
          } else {
            this.noty.error();
          }
        });
    // }
  }
  deleteAccount(playerId: any, index: number) {
    this.isLoading = true;
    this.spinner.start();
    this.http.delete(this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/accounts/' + playerId)
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
  openModal(template: TemplateRef<any>, account: any) {
    this.account = account;
    this.modalRef = this.modalService.show(template);
  }
  /*
  verifyAccount(playerId: any) {
    this.http.post(this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/accounts/' + playerId + '/verify', '')
      .subscribe(() => {
      this.accounts.find(a => a.playerId === playerId).isVerified = true;
      this.noty.success('A fiók sikeresen megerősítve');
    }, () => {
      this.noty.error();
    });
  }
  */
}
