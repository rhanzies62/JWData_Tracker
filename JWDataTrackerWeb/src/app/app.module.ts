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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ForgotPasswordComponent,
    DashboardComponent,
    NavMenuComponent,
    MainContentComponent,
    MidWeekSchedulePanelComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'forgotpassword', component: ForgotPasswordComponent, pathMatch: 'full' },
      {
        path: '', component: MainContentComponent, children: [
          { path: 'dashboard', component: DashboardComponent} 
        ]
      }
    ], { scrollPositionRestoration: 'enabled' })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }


