import {Component, OnInit} from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {NotyService} from '../_services/noty.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  providers: []
})
export class NavComponent implements OnInit {
  isCollapsed = true;
  staffRoles = ['Szuper admin', 'Fejlesztő', 'Admin'];

  constructor(public authService: AuthService, private noty: NotyService, private router: Router) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.noty.success('Sikeres kijelentkezés');
    this.router.navigate(['/']);
  }
}
