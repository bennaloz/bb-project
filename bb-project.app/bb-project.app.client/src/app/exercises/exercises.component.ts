import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ExerciseDefinition, ExerciseType, InvolvedMuscles } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  standalone: false
})
export class ExercisesComponent implements OnInit {
  exercises: ExerciseDefinition[] = [];

  dialogVisible = false;
  editingExercise: ExerciseDefinition | null = null;
  form: FormGroup;

  exerciseTypes = [
    { label: 'Cardio', value: ExerciseType.Cardio },
    { label: 'Weights', value: ExerciseType.Weights }
  ];

  muscleOptions = [
    { label: 'Pectorals',  value: InvolvedMuscles.Pectorals },
    { label: 'Back',       value: InvolvedMuscles.Back },
    { label: 'Deltoids',   value: InvolvedMuscles.Deltoids },
    { label: 'Quadriceps', value: InvolvedMuscles.Quadriceps },
    { label: 'Hamstrings', value: InvolvedMuscles.Hamstrings },
    { label: 'Calf',       value: InvolvedMuscles.Calf },
    { label: 'Biceps',     value: InvolvedMuscles.Biceps },
    { label: 'Triceps',    value: InvolvedMuscles.Triceps },
    { label: 'Heart',      value: InvolvedMuscles.Heart }
  ];

  constructor(
    private api: WorkoutsApiService,
    private userService: UserService,
    private messageService: MessageService,
    fb: FormBuilder
  ) {
    this.form = fb.group({
      name: ['', Validators.required],
      type: [ExerciseType.Cardio, Validators.required],
      involvedMuscles: [[]]
    });
  }

  ngOnInit(): void {
    this.loadExercises();
  }

  loadExercises(): void {
    const userId = this.userService.getCurrentUser();
    this.api.getExerciseDefinitions(userId).subscribe({
      next: ex => (this.exercises = ex),
      error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load exercises' })
    });
  }

  typeName(type: ExerciseType): string {
    return type === ExerciseType.Weights ? 'Weights' : 'Cardio';
  }

  muscleNames(mask: number): string {
    return this.muscleOptions.filter(m => (mask & m.value) !== 0).map(m => m.label).join(', ') || '—';
  }

  private bitmaskToArray(mask: number): number[] {
    return this.muscleOptions.filter(m => (mask & m.value) !== 0).map(m => m.value);
  }

  openAdd(): void {
    this.editingExercise = null;
    this.form.reset({ name: '', type: ExerciseType.Cardio, involvedMuscles: [] });
    this.dialogVisible = true;
  }

  openEdit(exercise: ExerciseDefinition): void {
    this.editingExercise = exercise;
    this.form.setValue({
      name: exercise.name,
      type: exercise.type,
      involvedMuscles: this.bitmaskToArray(exercise.involvedMuscles)
    });
    this.dialogVisible = true;
  }

  submit(): void {
    if (this.form.invalid) return;
    const value = this.form.value;
    const mask = (value.involvedMuscles as number[]).reduce((acc, v) => acc | v, 0);
    const payload = { name: value.name, type: value.type, involvedMuscles: mask };
    if (this.editingExercise) {
      this.api.updateExercise(this.editingExercise.id, payload).subscribe({
        next: () => { this.dialogVisible = false; this.loadExercises(); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update exercise' })
      });
    } else {
      const userId = this.userService.getCurrentUser();
      this.api.createExercise(userId, payload).subscribe({
        next: () => { this.dialogVisible = false; this.loadExercises(); },
        error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create exercise' })
      });
    }
  }
}
