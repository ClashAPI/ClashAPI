import {Component, OnInit} from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {NotyService} from '../_services/noty.service';
import {Router} from '@angular/router';
import {NgxUiLoaderService} from 'ngx-ui-loader';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isLoading = false;
  model: any = {};

  constructor(private authService: AuthService, private noty: NotyService, private router: Router, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
  }

  login() {
    this.isLoading = true;
    this.spinner.start();
    this.authService.login(this.model)
      .pipe(finalize(() => {
        this.isLoading = false;
        this.spinner.stop();
      }))
      .subscribe(next => {
        this.noty.success('Sikeres bejelentkezés');
      },
        (error) => {
        this.noty.error();
        console.log(error);
      }, () => {
        this.router.navigate(['/']);
      }
    );
  }
}
