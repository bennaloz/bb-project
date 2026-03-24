import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Workout, WorkoutPlan } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrl: './plans.component.css',
  standalone: false
})
export class PlansComponent implements OnInit {
  plans: WorkoutPlan[] = [];
  workoutsMap: Record<number, Workout[]> = {};
  workoutColumns = ['order', 'name', 'actions'];

  // Plan dialog
  planDialogVisible = false;
  editingPlan: WorkoutPlan | null = null;
  planForm: FormGroup;

  // Workout dialog
  workoutDialogVisible = false;
  editingWorkout: Workout | null = null;
  currentPlanId = 0;
  workoutForm: FormGroup;

  constructor(
    private api: WorkoutsApiService,
    private userService: UserService,
    private messageService: MessageService,
    fb: FormBuilder
  ) {
    this.planForm = fb.group({
      name: ['', Validators.required],
      isActive: [false]
    });
    this.workoutForm = fb.group({
      name: ['', Validators.required],
      order: [0]
    });
  }

  ngOnInit(): void {
    this.loadPlans();
  }

  loadPlans(): void {
    const userId = this.userService.getCurrentUser();
    this.api.getPlans(userId).subscribe({
      next: plans => (this.plans = plans),
      error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load plans' })
    });
  }

  onPanelOpen(event: { index: number }): void {
    // event.index is the [value] bound to p-accordion-panel, which equals plan.id
    this.loadWorkoutsForPlan(event.index);
  }

  loadWorkoutsForPlan(planId: number): void {
    this.api.getWorkouts(planId).subscribe({
      next: w => (this.workoutsMap = { ...this.workoutsMap, [planId]: w }),
      error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load workouts' })
    });
  }

  // ── Plan CRUD ──────────────────────────────────────────────────────────────

  openAddPlan(): void {
    this.editingPlan = null;
    this.planForm.reset({ name: '', isActive: false });
    this.planDialogVisible = true;
  }

  openEditPlan(plan: WorkoutPlan): void {
    this.editingPlan = plan;
    this.planForm.setValue({ name: plan.name, isActive: plan.isActive });
    this.planDialogVisible = true;
  }

  submitPlan(): void {
    if (this.planForm.invalid) return;
    const userId = this.userService.getCurrentUser();
    const value = this.planForm.value;
    if (this.editingPlan) {
      this.api.updatePlan(this.editingPlan.id, userId, value).subscribe({
        next: () => { this.planDialogVisible = false; this.loadPlans(); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update plan' })
      });
    } else {
      this.api.createPlan(userId, value).subscribe({
        next: () => { this.planDialogVisible = false; this.loadPlans(); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create plan' })
      });
    }
  }

  // ── Workout CRUD ──────────────────────────────────────────────────────────

  openAddWorkout(plan: WorkoutPlan): void {
    this.editingWorkout = null;
    this.currentPlanId = plan.id;
    this.workoutForm.reset({ name: '', order: 0 });
    this.workoutDialogVisible = true;
  }

  openEditWorkout(plan: WorkoutPlan, workout: Workout): void {
    this.editingWorkout = workout;
    this.currentPlanId = plan.id;
    this.workoutForm.setValue({ name: workout.name, order: workout.order });
    this.workoutDialogVisible = true;
  }

  submitWorkout(): void {
    if (this.workoutForm.invalid) return;
    const value = this.workoutForm.value;
    if (this.editingWorkout) {
      this.api.updateWorkout(this.editingWorkout.id, this.currentPlanId, value).subscribe({
        next: () => { this.workoutDialogVisible = false; this.loadWorkoutsForPlan(this.currentPlanId); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update workout' })
      });
    } else {
      this.api.createWorkout(this.currentPlanId, value).subscribe({
        next: () => { this.workoutDialogVisible = false; this.loadWorkoutsForPlan(this.currentPlanId); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create workout' })
      });
    }
  }
}
