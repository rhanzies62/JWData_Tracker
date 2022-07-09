import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';
import { MainContentComponent } from './pages/main-content/main-content.component';
import { MidWeekSchedulePanelComponent } from './pages/dashboard/mid-week-schedule-panel/mid-week-schedule-panel.component';
import { WeekendMeetingSchedulePanelComponent } from './pages/dashboard/weekend-meeting-schedule-panel/weekend-meeting-schedule-panel.component';
import { CongregationStatsComponent } from './pages/dashboard/congregation-stats/congregation-stats.component';
import { PublisherAnalysisPanelComponent } from './pages/dashboard/publisher-analysis-panel/publisher-analysis-panel.component';
import { MeetingAttendanceSummaryPanelComponent } from './pages/dashboard/meeting-attendance-summary-panel/meeting-attendance-summary-panel.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { AuthenticationGuardService } from "./core/services/authguard.service";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { AuthenticateApiService } from './core/apiService/authenticate-api.service';
import { HttpClientModule } from '@angular/common/http';
import { LoadingScreenComponent } from './shared/components/loading-screen/loading-screen.component';
import { BaseModuleComponent } from './pages/base-module/base-module.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ForgotPasswordComponent,
    DashboardComponent,
    NavMenuComponent,
    MainContentComponent,
    MidWeekSchedulePanelComponent,
    WeekendMeetingSchedulePanelComponent,
    CongregationStatsComponent,
    PublisherAnalysisPanelComponent,
    MeetingAttendanceSummaryPanelComponent,
    LoadingScreenComponent,
    BaseModuleComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'forgotpassword', component: ForgotPasswordComponent, pathMatch: 'full' },
      {
        path: '', component: MainContentComponent,canActivate: [AuthenticationGuardService] , children: [
          { path: 'dashboard', component: DashboardComponent, canActivate: [AuthenticationGuardService] } 
        ]
      }
    ], { scrollPositionRestoration: 'enabled' }),
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [{ provide: JWT_OPTIONS, useValue: JWT_OPTIONS },JwtHelperService, AuthenticateApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }


