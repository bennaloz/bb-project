import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom } from 'rxjs';

export interface SeriesDataEntry {
  exerciseId: number;
  serieId: number;
  startTime: Date;
  endTime: Date;
  usedKgs: number | null;
}

export interface WorkoutSession {
  workoutId: number;
  workoutPlanId: number;
  userId: string;
  startDate: Date;
  seriesData: SeriesDataEntry[];
}

@Injectable({
  providedIn: 'root'
})
export class WorkoutSessionService {
  private readonly activeSession$ = new BehaviorSubject<WorkoutSession | null>(null);

  readonly session$ = this.activeSession$.asObservable();

  get currentSession(): WorkoutSession | null {
    return this.activeSession$.value;
  }

  get isSessionActive(): boolean {
    return this.activeSession$.value !== null;
  }

  constructor(private http: HttpClient) {}

  startSession(workoutId: number, workoutPlanId: number, userId: string): void {
    this.activeSession$.next({
      workoutId,
      workoutPlanId,
      userId,
      startDate: new Date(),
      seriesData: []
    });
  }

  addSeriesData(entry: SeriesDataEntry): void {
    const session = this.activeSession$.value;
    if (!session) {
      throw new Error('No active workout session.');
    }
    this.activeSession$.next({
      ...session,
      seriesData: [...session.seriesData, entry]
    });
  }

  async saveSession(): Promise<void> {
    const session = this.activeSession$.value;
    if (!session) {
      throw new Error('No active workout session to save.');
    }

    const endDate = new Date();

    const workoutHistoryId = await firstValueFrom(
      this.http.post<number>('/WorkoutsEditor/insertWOHistory', {
        startDate: session.startDate,
        endDate,
        workoutId: session.workoutId,
        workoutPlanId: session.workoutPlanId,
        userId: session.userId
      })
    );

    if (session.seriesData.length > 0) {
      await firstValueFrom(
        this.http.post('/WorkoutsEditor/insertWOData', {
          workoutHistoryId,
          seriesData: session.seriesData.map(s => ({
            exerciseId: s.exerciseId,
            serieId: s.serieId,
            startTime: s.startTime,
            endTime: s.endTime,
            usedKgs: s.usedKgs
          }))
        })
      );
    }

    this.activeSession$.next(null);
  }

  discardSession(): void {
    this.activeSession$.next(null);
  }
}
