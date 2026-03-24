import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { WorkoutPlan } from '../models';

@Component({
  selector: 'app-plan-dialog',
  templateUrl: './plan-dialog.component.html',
  standalone: false
})
export class PlanDialogComponent {
  form: FormGroup;

  constructor(
    fb: FormBuilder,
    public dialogRef: MatDialogRef<PlanDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { plan?: WorkoutPlan }
  ) {
    this.form = fb.group({
      name: [data?.plan?.name ?? '', Validators.required],
      isActive: [data?.plan?.isActive ?? false]
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
