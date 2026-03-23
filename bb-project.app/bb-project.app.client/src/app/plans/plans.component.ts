import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { WorkoutPlan } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';
import { PlanDialogComponent } from './plan-dialog.component';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  standalone: false
})
export class PlansComponent implements OnInit {
  plans: WorkoutPlan[] = [];
  displayedColumns = ['id', 'name', 'isActive', 'actions'];

  constructor(
    private api: WorkoutsApiService,
    private userService: UserService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: Router
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

  openEdit(plan: WorkoutPlan): void {
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

  viewWorkouts(plan: WorkoutPlan): void {
    this.router.navigate(['/plans', plan.id, 'workouts']);
  }
}
