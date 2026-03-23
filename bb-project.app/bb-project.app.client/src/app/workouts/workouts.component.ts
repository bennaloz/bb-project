import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Workout } from '../models';
import { WorkoutsApiService } from '../workouts-api.service';
import { WorkoutDialogComponent } from './workout-dialog.component';

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  standalone: false
})
export class WorkoutsComponent implements OnInit {
  planId = 0;
  workouts: Workout[] = [];
  displayedColumns = ['id', 'name', 'order', 'actions'];

  constructor(
    private route: ActivatedRoute,
    private api: WorkoutsApiService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.planId = Number(this.route.snapshot.paramMap.get('planId'));
    this.loadWorkouts();
  }

  loadWorkouts(): void {
    this.api.getWorkouts(this.planId).subscribe({
      next: w => (this.workouts = w),
      error: () => this.snackBar.open('Failed to load workouts', 'Close', { duration: 3000 })
    });
  }

  openAdd(): void {
    const ref = this.dialog.open(WorkoutDialogComponent, { data: {} });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      this.api.createWorkout(this.planId, result).subscribe({
        next: () => this.loadWorkouts(),
        error: () => this.snackBar.open('Failed to create workout', 'Close', { duration: 3000 })
      });
    });
  }

  openEdit(workout: Workout): void {
    const ref = this.dialog.open(WorkoutDialogComponent, { data: { workout } });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      this.api.updateWorkout(workout.id, this.planId, result).subscribe({
        next: () => this.loadWorkouts(),
        error: () => this.snackBar.open('Failed to update workout', 'Close', { duration: 3000 })
      });
    });
  }
}
