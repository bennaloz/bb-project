import { Component, OnInit } from '@angular/core';
import { WorkoutHistoryItem } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  standalone: false
})
export class HistoryComponent implements OnInit {
  history: WorkoutHistoryItem[] = [];
  displayedColumns = ['userId', 'workoutId', 'startDate', 'endDate'];

  constructor(
    private api: WorkoutsApiService,
    private userService: UserService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const userId = this.userService.getCurrentUser();
    this.api.getHistory(userId).subscribe({
      next: h => (this.history = h),
      error: () => this.snackBar.open('Failed to load history', 'Close', { duration: 3000 })
    });
  }
}
