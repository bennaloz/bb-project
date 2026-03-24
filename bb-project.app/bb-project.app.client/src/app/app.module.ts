import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { MessageService } from 'primeng/api';
import { SharedModule } from 'primeng/api';
import { AccordionModule } from 'primeng/accordion';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';
import { DividerModule } from 'primeng/divider';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { ListboxModule } from 'primeng/listbox';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { SelectModule } from 'primeng/select';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { TooltipModule } from 'primeng/tooltip';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsersComponent } from './users/users.component';
import { PlansComponent } from './plans/plans.component';
import { WorkoutsComponent } from './workouts/workouts.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { HistoryComponent } from './history/history.component';
import { UserService } from './user.service';

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    PlansComponent,
    WorkoutsComponent,
    ExercisesComponent,
    HistoryComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    SharedModule,
    AccordionModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
    DialogModule,
    DividerModule,
    FloatLabelModule,
    InputTextModule,
    ListboxModule,
    MultiSelectModule,
    ProgressSpinnerModule,
    SelectModule,
    TableModule,
    TagModule,
    TextareaModule,
    ToastModule,
    ToolbarModule,
    TooltipModule,
  ],
  providers: [
    UserService,
    MessageService,
    providePrimeNG({ theme: { preset: Aura } }),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
