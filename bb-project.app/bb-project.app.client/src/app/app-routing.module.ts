import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { PlansComponent } from './plans/plans.component';
import { WorkoutsComponent } from './workouts/workouts.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { HistoryComponent } from './history/history.component';

const routes: Routes = [
  { path: '', redirectTo: '/plans', pathMatch: 'full' },
  { path: 'users', component: UsersComponent },
  { path: 'plans', component: PlansComponent },
  { path: 'plans/:planId/workouts', component: WorkoutsComponent },
  { path: 'exercises', component: ExercisesComponent },
  { path: 'history', component: HistoryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
