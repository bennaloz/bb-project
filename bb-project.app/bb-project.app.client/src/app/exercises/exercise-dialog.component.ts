import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ExerciseDefinition, ExerciseType, InvolvedMuscles } from '../models';

@Component({
  selector: 'app-exercise-dialog',
  templateUrl: './exercise-dialog.component.html',
  standalone: false
})
export class ExerciseDialogComponent {
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
    fb: FormBuilder,
    public dialogRef: MatDialogRef<ExerciseDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { exercise?: ExerciseDefinition }
  ) {
    const selectedMuscles = this.bitmaskToArray(data?.exercise?.involvedMuscles ?? 0);
    this.form = fb.group({
      name: [data?.exercise?.name ?? '', Validators.required],
      type: [data?.exercise?.type ?? ExerciseType.Cardio, Validators.required],
      involvedMuscles: [selectedMuscles]
    });
  }

  private bitmaskToArray(mask: number): number[] {
    return this.muscleOptions.filter(m => (mask & m.value) !== 0).map(m => m.value);
  }

  submit(): void {
    if (this.form.valid) {
      const value = this.form.value;
      const mask = (value.involvedMuscles as number[]).reduce((acc, v) => acc | v, 0);
      this.dialogRef.close({ name: value.name, type: value.type, involvedMuscles: mask });
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
