import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {NotyService} from '../_services/noty.service';
import {Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  baseUrl = environment.apiUrl;

  constructor(private authService: AuthService, private noty: NotyService, private router: Router, private http: HttpClient) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe((user) => {
      this.noty.success('Sikeres regisztráció');
      this.authService.login(this.model).subscribe(() => {
        this.router.navigate(['/']);
      }, error => {
        this.noty.error('Az automatikus bejelentkeztetés sikertelen');
      });
    }, error => {
      this.noty.error('Sikertelen regisztráció');
    });
  }
}
