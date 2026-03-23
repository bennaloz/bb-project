import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Workout } from '../models';

@Component({
  selector: 'app-workout-dialog',
  templateUrl: './workout-dialog.component.html',
  standalone: false
})
export class WorkoutDialogComponent {
  form: FormGroup;

  constructor(
    fb: FormBuilder,
    public dialogRef: MatDialogRef<WorkoutDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { workout?: Workout }
  ) {
    this.form = fb.group({
      name: [data?.workout?.name ?? '', Validators.required],
      order: [data?.workout?.order ?? 0]
    });
  }

  submit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
