import {BrowserModule} from '@angular/platform-browser';
import {ErrorHandler, Injectable, NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {NavComponent} from './nav/nav.component';
import {HomeComponent} from './home/home.component';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {RouterModule} from '@angular/router';
import {appRoutes} from './routes';
import {PlayerDetailsComponent} from './player/player-details/player-details.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {PlayerArenaComponent} from './player/player-arena/player-arena.component';
import {PlayerChestsComponent} from './player/player-chests/player-chests.component';
import {PlayerDeckComponent} from './player/player-deck/player-deck.component';
import {ProgressbarModule} from 'ngx-bootstrap/progressbar';
import {FooterComponent} from './footer/footer.component';
import {DatePipe, HashLocationStrategy, LocationStrategy} from '@angular/common';
import {PlayerClanRoleComponent} from './player/player-clan-role/player-clan-role.component';
import {DataTablesModule} from 'angular-datatables';
import {ClanDetailsComponent} from './clan/clan-details/clan-details.component';
import {ClanTypeComponent} from './clan/clan-type/clan-type.component';
import {MomentModule} from 'ngx-moment';
import {PlayerBattlesComponent} from './player/player-battles/player-battles.component';
import {PlayerHeaderComponent} from './player/player-header/player-header.component';
import {NgxUiLoaderConfig, NgxUiLoaderModule, SPINNER} from 'ngx-ui-loader';
import {CarouselModule, PopoverModule, TabsModule} from 'ngx-bootstrap';

import * as Sentry from '@sentry/browser';
import {HasRoleDirective} from './_directives/has-role.directive';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {FormsModule} from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {QueryPlayerComponent} from './query/query-player/query-player.component';
import {QueryClanComponent} from './query/query-clan/query-clan.component';
import {UserProfileComponent} from './user/user-profile/user-profile.component';
import {JwtModule} from '@auth0/angular-jwt';
import {AdminUsersComponent} from './admin/admin-users/admin-users.component';
import {ErrorInterceptor} from './_services/error.interceptor';
import {AuthService} from './_services/auth.service';
import {AdminHealthComponent} from './admin/admin-health/admin-health.component';
import {ModalModule} from 'ngx-bootstrap/modal';
import {QueryInfoComponent} from './query/query-info/query-info.component';
import {FavoritePlayersComponent} from './favorites/favorite-players/favorite-players.component';
import {FavoriteClansComponent} from './favorites/favorite-clans/favorite-clans.component';
import {FavoriteDecksComponent} from './favorites/favorite-decks/favorite-decks.component';
import {FavoritesComponent} from './favorites/favorites/favorites.component';
import {PlayerClanBadgeComponent} from './player/player-clan-badge/player-clan-badge.component';
import {ClanBadgeComponent} from './clan/clan-badge/clan-badge.component';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {ServiceWorkerModule} from '@angular/service-worker';
import {environment} from '../environments/environment';
import {AdminPostsComponent} from './admin/admin-posts/admin-posts.component';
import {CKEditorModule} from '@ckeditor/ckeditor5-angular';
import {UserCrAccountsComponent} from './user/user-cr-accounts/user-cr-accounts.component';
import {AdminCrAccountsComponent} from './admin/admin-cr-accounts/admin-cr-accounts.component';
import {AdminAnnouncementsComponent} from './admin/admin-announcements/admin-announcements.component';
import {AnnouncementsComponent} from './announcements/announcements.component';
import {NgxSelectModule} from 'ngx-select-ex';
import {PlayerLeaderboardComponent} from './player/player-leaderboard/player-leaderboard.component';
import {LazyLoadImageModule} from 'ng-lazyload-image';
import {PlayerArenaNameComponent} from './player/player-arena-name/player-arena-name.component';


Sentry.init({
  dsn: 'https://4f24ad366e7f4335b0de81c541269f3e@sentry.io/1874117'
});

@Injectable()
export class SentryErrorHandler implements ErrorHandler {
  constructor() {}
  handleError(error) {
    const eventId = Sentry.captureException(error.originalError || error);
    Sentry.showReportDialog({ eventId });
  }
}

export function tokenGetter() {
  return localStorage.getItem('token');
}

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  fgsColor: '#3498db',
  fgsSize: 70,
  fgsType: SPINNER.cubeGrid, // foreground spinner type
  pbColor: '#3498db',
  overlayColor: '#ffffff',
  hasProgressBar: false,
  text: 'Betöltés folyamatban...',
  textColor: '#000000',
  minTime: -1,
  maxTime: -1,
  delay: 0,
};

@NgModule({
    declarations: [
        AppComponent,
        AnnouncementsComponent,
        NavComponent,
        FooterComponent,
        HomeComponent,
        QueryPlayerComponent,
        QueryClanComponent,
        PlayerDetailsComponent,
        PlayerArenaComponent,
        PlayerArenaNameComponent,
        PlayerChestsComponent,
        PlayerDeckComponent,
        PlayerBattlesComponent,
        PlayerClanRoleComponent,
        PlayerClanBadgeComponent,
        PlayerHeaderComponent,
        PlayerLeaderboardComponent,
        ClanDetailsComponent,
        ClanTypeComponent,
        ClanBadgeComponent,
        HasRoleDirective,
        LoginComponent,
        RegisterComponent,
        UserProfileComponent,
        UserCrAccountsComponent,
        AdminAnnouncementsComponent,
        AdminPostsComponent,
        AdminUsersComponent,
        AdminCrAccountsComponent,
        AdminHealthComponent,
        QueryInfoComponent,
        FavoritesComponent,
        FavoritePlayersComponent,
        FavoriteClansComponent,
        FavoriteDecksComponent,
    ],
    imports: [
        RouterModule.forRoot(appRoutes, {onSameUrlNavigation: 'reload'}),
        BrowserModule.withServerTransition({appId: 'serverApp'}),
        BrowserAnimationsModule,
        HttpClientModule,
        JwtModule.forRoot({
            config: {
                tokenGetter,
                whitelistedDomains: ['localhost:5001'],
                blacklistedRoutes: ['localhost:5001/api/auth'],
            }
        }),
        MomentModule,
        BsDropdownModule.forRoot(),
        ProgressbarModule.forRoot(),
        DataTablesModule,
        NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
        // NgxUiLoaderHttpModule.forRoot({showForeground: true}),
        CarouselModule,
        FormsModule,
        TabsModule.forRoot(),
        ModalModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        CKEditorModule,
        NgxSelectModule,
        LazyLoadImageModule,
        ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
    ],
  providers: [
    AuthService,
    DatePipe,
    ErrorInterceptor,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
