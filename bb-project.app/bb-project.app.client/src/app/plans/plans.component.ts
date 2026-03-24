import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Workout, WorkoutPlan } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';
import { PlanDialogComponent } from './plan-dialog.component';
import { WorkoutDialogComponent } from '../workouts/workout-dialog.component';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrl: './plans.component.css',
  standalone: false
})
export class PlansComponent implements OnInit {
  plans: WorkoutPlan[] = [];
  /** Lazy-loaded workouts keyed by plan id */
  workoutsMap: Record<number, Workout[]> = {};
  workoutColumns = ['order', 'name', 'actions'];

  constructor(
    private api: WorkoutsApiService,
    private userService: UserService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadPlans();
  }

  loadPlans(): void {
    this.api.getPlans().subscribe({
      next: plans => (this.plans = plans),
      error: () => this.snackBar.open('Failed to load plans', 'Close', { duration: 3000 })
    });
  }

  /** Called when an expansion panel opens — load workouts for that plan if not cached. */
  onPlanOpened(plan: WorkoutPlan): void {
    this.loadWorkoutsForPlan(plan.id);
  }

  loadWorkoutsForPlan(planId: number): void {
    this.api.getWorkouts(planId).subscribe({
      next: w => (this.workoutsMap = { ...this.workoutsMap, [planId]: w }),
      error: () => this.snackBar.open('Failed to load workouts', 'Close', { duration: 3000 })
    });
  }

  // ── Plan CRUD ──────────────────────────────────────────────────────────────

  openAdd(): void {
    const ref = this.dialog.open(PlanDialogComponent, { data: {} });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      const userId = this.userService.getCurrentUser();
      this.api.createPlan(userId, result).subscribe({
        next: () => this.loadPlans(),
        error: () => this.snackBar.open('Failed to create plan', 'Close', { duration: 3000 })
      });
    });
  }

  openEdit(plan: WorkoutPlan, event: Event): void {
    event.stopPropagation();
    const ref = this.dialog.open(PlanDialogComponent, { data: { plan } });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      const userId = this.userService.getCurrentUser();
      this.api.updatePlan(plan.id, userId, result).subscribe({
        next: () => this.loadPlans(),
        error: () => this.snackBar.open('Failed to update plan', 'Close', { duration: 3000 })
      });
    });
  }

  // ── Workout CRUD (inline within each plan) ────────────────────────────────

  openAddWorkout(plan: WorkoutPlan, event: Event): void {
    event.stopPropagation();
    const ref = this.dialog.open(WorkoutDialogComponent, { data: {} });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      this.api.createWorkout(plan.id, result).subscribe({
        next: () => this.loadWorkoutsForPlan(plan.id),
        error: () => this.snackBar.open('Failed to create workout', 'Close', { duration: 3000 })
      });
    });
  }

  openEditWorkout(plan: WorkoutPlan, workout: Workout, event: Event): void {
    event.stopPropagation();
    const ref = this.dialog.open(WorkoutDialogComponent, { data: { workout } });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      this.api.updateWorkout(workout.id, plan.id, result).subscribe({
        next: () => this.loadWorkoutsForPlan(plan.id),
        error: () => this.snackBar.open('Failed to update workout', 'Close', { duration: 3000 })
      });
    });
  }
}
