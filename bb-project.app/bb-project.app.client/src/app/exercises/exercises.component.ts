import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ExerciseDefinition, ExerciseType, InvolvedMuscles } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';
import { ExerciseDialogComponent } from './exercise-dialog.component';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  standalone: false
})
export class ExercisesComponent implements OnInit {
  exercises: ExerciseDefinition[] = [];
  displayedColumns = ['id', 'name', 'type', 'involvedMuscles', 'actions'];

  private muscleMap: { label: string; value: number }[] = [
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
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadExercises();
  }

  loadExercises(): void {
    const userId = this.userService.getCurrentUser();
    this.api.getExerciseDefinitions(userId).subscribe({
      next: ex => (this.exercises = ex),
      error: () => this.snackBar.open('Failed to load exercises', 'Close', { duration: 3000 })
    });
  }

  typeName(type: ExerciseType): string {
    return type === ExerciseType.Weights ? 'Weights' : 'Cardio';
  }

  muscleNames(mask: number): string {
    return this.muscleMap.filter(m => (mask & m.value) !== 0).map(m => m.label).join(', ') || '—';
  }

  openAdd(): void {
    const ref = this.dialog.open(ExerciseDialogComponent, { data: {} });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      const userId = this.userService.getCurrentUser();
      this.api.createExercise(userId, result).subscribe({
        next: () => this.loadExercises(),
        error: () => this.snackBar.open('Failed to create exercise', 'Close', { duration: 3000 })
      });
    });
  }

  openEdit(exercise: ExerciseDefinition): void {
    const ref = this.dialog.open(ExerciseDialogComponent, { data: { exercise } });
    ref.afterClosed().subscribe(result => {
      if (!result) return;
      this.api.updateExercise(exercise.id, result).subscribe({
        next: () => this.loadExercises(),
        error: () => this.snackBar.open('Failed to update exercise', 'Close', { duration: 3000 })
      });
    });
  }
}
