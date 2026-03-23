import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WorkoutSessionComponent } from './components/workout-session/workout-session.component';

@NgModule({
  declarations: [
    AppComponent,
    WorkoutSessionComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, CommonModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
