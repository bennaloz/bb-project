import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Workout } from '../models';
import { WorkoutsApiService } from '../workouts-api.service';

@Component({
  selector: 'app-workouts',
  templateUrl: './workouts.component.html',
  standalone: false
})
export class WorkoutsComponent implements OnInit {
  planId = 0;
  planName = '';
  workouts: Workout[] = [];

  dialogVisible = false;
  editingWorkout: Workout | null = null;
  form: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private api: WorkoutsApiService,
    private messageService: MessageService,
    fb: FormBuilder
  ) {
    this.form = fb.group({
      name: ['', Validators.required],
      order: [0]
    });
  }

  ngOnInit(): void {
    this.planId = Number(this.route.snapshot.paramMap.get('planId'));
    this.api.getPlans(String(this.planId)).subscribe({
      next: plans => { if (plans.length) this.planName = plans[0].name; }
    });
    this.loadWorkouts();
  }

  loadWorkouts(): void {
    this.api.getWorkouts(this.planId).subscribe({
      next: w => (this.workouts = w),
      error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load workouts' })
    });
  }

  backToPlans(): void {
    this.router.navigate(['/plans']);
  }

  openAdd(): void {
    this.editingWorkout = null;
    this.form.reset({ name: '', order: 0 });
    this.dialogVisible = true;
  }

  openEdit(workout: Workout): void {
    this.editingWorkout = workout;
    this.form.setValue({ name: workout.name, order: workout.order });
    this.dialogVisible = true;
  }

  submit(): void {
    if (this.form.invalid) return;
    const value = this.form.value;
    if (this.editingWorkout) {
      this.api.updateWorkout(this.editingWorkout.id, this.planId, value).subscribe({
        next: () => { this.dialogVisible = false; this.loadWorkouts(); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update workout' })
      });
    } else {
      this.api.createWorkout(this.planId, value).subscribe({
        next: () => { this.dialogVisible = false; this.loadWorkouts(); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create workout' })
      });
    }
  }
}
