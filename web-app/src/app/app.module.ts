import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './_components/home/home.component';
import { UserComponent } from './_components/user/user.component';
import { AuthModule } from './_components/auth/auth.module';

@NgModule({
  declarations: [AppComponent, HomeComponent, UserComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, AuthModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
