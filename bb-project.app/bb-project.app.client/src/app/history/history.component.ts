import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { WorkoutHistoryItem } from '../models';
import { UserService } from '../user.service';
import { WorkoutsApiService } from '../workouts-api.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  standalone: false
})
export class HistoryComponent implements OnInit {
  history: WorkoutHistoryItem[] = [];

  constructor(
    private api: WorkoutsApiService,
    private userService: UserService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    const userId = this.userService.getCurrentUser();
    this.api.getHistory(userId).subscribe({
      next: h => (this.history = h),
      error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load history' })
    });
  }
}
