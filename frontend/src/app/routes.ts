import {Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';
import {PlayerDetailsComponent} from './player/player-details/player-details.component';
import {ClanDetailsComponent} from './clan/clan-details/clan-details.component';
import {PlayerBattlesComponent} from './player/player-battles/player-battles.component';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {QueryPlayerComponent} from './query/query-player/query-player.component';
import {QueryClanComponent} from './query/query-clan/query-clan.component';
import {UserProfileComponent} from './user/user-profile/user-profile.component';
import {AuthGuard} from './_guards/auth.guard';
import {AdminUsersComponent} from './admin/admin-users/admin-users.component';
import {AdminHealthComponent} from './admin/admin-health/admin-health.component';
import {QueryInfoComponent} from './query/query-info/query-info.component';
import {FavoritesComponent} from './favorites/favorites/favorites.component';
import {AdminPostsComponent} from './admin/admin-posts/admin-posts.component';
import {UserCrAccountsComponent} from './user/user-cr-accounts/user-cr-accounts.component';
import {AdminCrAccountsComponent} from './admin/admin-cr-accounts/admin-cr-accounts.component';
import {AdminAnnouncementsComponent} from './admin/admin-announcements/admin-announcements.component';
import {PlayerLeaderboardComponent} from './player/player-leaderboard/player-leaderboard.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'query/info', component: QueryInfoComponent },
  { path: 'query/player', component: QueryPlayerComponent },
  { path: 'query/clan', component: QueryClanComponent },
  { path: 'player/:id', component: PlayerDetailsComponent },
  { path: 'player/:id/battles', component: PlayerBattlesComponent },
  { path: 'leaderboard/players', component: PlayerLeaderboardComponent },
  { path: 'clan/:id', component: ClanDetailsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'profile', component: UserProfileComponent },
      { path: 'accounts', component: UserCrAccountsComponent },
      { path: 'follows', component: FavoritesComponent },
      { path: 'admin/users', component: AdminUsersComponent, data: { roles: ['Fejlesztő'] } },
      { path: 'admin/accounts', component: AdminCrAccountsComponent, data: { roles: ['Fejlesztő'] } },
      { path: 'admin/posts', component: AdminPostsComponent, data: { roles: ['Fejlesztő'] } },
      { path: 'admin/announcements', component: AdminAnnouncementsComponent, data: { roles: ['Fejlesztő'] } },
      { path: 'admin', component: AdminHealthComponent, data: { roles: ['Fejlesztő'] } },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
