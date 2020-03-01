import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router} from '@angular/router';
import {AuthService} from '../_services/auth.service';
import {NotyService} from '../_services/noty.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private noty: NotyService) {
  }

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const roles = next.firstChild.data['roles'] as Array<string>;
    if (roles) {
      const match = this.authService.roleMatch(roles);

      if (match) {
        return true;
      } else {
        this.router.navigate(['/']);
        this.noty.error('Elégtelen jogosultsági szint');
        // this.noty.warning('Kísérlet naplózva');
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.noty.info('A kért oldal eléréséhez előbb be kell jelentkezned');
    this.router.navigate(['/']);
    return false;
  }
}
