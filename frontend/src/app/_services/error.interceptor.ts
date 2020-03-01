import {Injectable} from '@angular/core';
import {HTTP_INTERCEPTORS, HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {NotyService} from './noty.service';
import {Router} from '@angular/router';
import {AuthService} from './auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthService, private noty: NotyService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401) {
          if (this.authService.loggedIn()) {
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            this.authService.decodedToken = null;
            this.authService.currentUser = null;
            this.noty.warning('Automatikusan kijelentkeztetésre kerültél');
            this.router.navigate(['/']);
          }
          return throwError(error.statusText);
        }
        if (error instanceof HttpErrorResponse) {
          const applicationError = error.headers.get('Application-Error');
          if (applicationError) {
            return throwError(applicationError);
          }
          const serverError = error.error;
          let modalStateErrors = '';
          if (serverError.errors && typeof serverError.errors === 'object') {
            for (const key in serverError.errors) {
              if (serverError.errors[key]) {
                modalStateErrors += serverError.errors[key] + '\n';
              }
            }
          }
          return throwError(modalStateErrors || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
