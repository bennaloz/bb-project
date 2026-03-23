import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SeriesDataEntry, WorkoutSession, WorkoutSessionService } from '../../services/workout-session.service';

@Component({
  selector: 'app-workout-session',
  standalone: false,
  templateUrl: './workout-session.component.html',
  styleUrl: './workout-session.component.css'
})
export class WorkoutSessionComponent implements OnInit, OnDestroy {
  session: WorkoutSession | null = null;
  isSaving = false;
  statusMessage = '';

  // Demo form fields
  workoutId = 1;
  workoutPlanId = 1;
  userId = 'demo-user';

  serieId = 1;
  exerciseId = 1;
  usedKgs: number | null = null;

  private sessionSubscription: Subscription | null = null;

  constructor(private sessionService: WorkoutSessionService) {}

  ngOnInit(): void {
    this.sessionSubscription = this.sessionService.session$.subscribe(s => {
      this.session = s;
    });
  }

  ngOnDestroy(): void {
    this.sessionSubscription?.unsubscribe();
  }

  startSession(): void {
    this.statusMessage = '';
    this.sessionService.startSession(this.workoutId, this.workoutPlanId, this.userId);
    this.statusMessage = 'Session started. Changes will be saved when you click "Save Session".';
  }

  addEntry(): void {
    const now = new Date();
    const entry: SeriesDataEntry = {
      exerciseId: this.exerciseId,
      serieId: this.serieId,
      startTime: new Date(now.getTime() - 60000),
      endTime: now,
      usedKgs: this.usedKgs
    };
    this.sessionService.addSeriesData(entry);
    this.statusMessage = `Series entry added (exercise ${this.exerciseId}, serie ${this.serieId}).`;
  }

  async saveSession(): Promise<void> {
    this.isSaving = true;
    this.statusMessage = '';
    try {
      await this.sessionService.saveSession();
      this.statusMessage = 'Session saved successfully!';
    } catch (err) {
      this.statusMessage = 'Error saving session: ' + (err as Error).message;
    } finally {
      this.isSaving = false;
    }
  }

  discardSession(): void {
    this.sessionService.discardSession();
    this.statusMessage = 'Session discarded. All unsaved changes have been removed.';
  }
}
