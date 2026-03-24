import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ExerciseDefinition, Workout, WorkoutHistoryItem, WorkoutPlan } from './models';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class WorkoutsApiService {
  private readonly base = `${environment.apiBase}/WorkoutsEditor`;

  constructor(private http: HttpClient) {}

  getPlans(userId = '', workoutPlanId = ''): Observable<WorkoutPlan[]> {
    const params: Record<string, string> = {};
    if (userId) params['userId'] = userId;
    if (workoutPlanId) params['workoutPlanId'] = workoutPlanId;
    return this.http.get<WorkoutPlan[]>(`${this.base}/plans`, { params });
  }

  createPlan(userId: string, plan: Partial<WorkoutPlan>): Observable<number> {
    return this.http.post<number>(`${this.base}/plans`, plan, { params: { userId } });
  }

  updatePlan(id: number, userId: string, plan: Partial<WorkoutPlan>): Observable<number> {
    return this.http.put<number>(`${this.base}/plans/${id}`, { userId, ...plan });
  }

  getWorkouts(planId: number): Observable<Workout[]> {
    return this.http.get<Workout[]>(`${this.base}/workouts`, { params: { workoutPlanId: planId } });
  }

  createWorkout(planId: number, workout: Partial<Workout>): Observable<number> {
    return this.http.post<number>(`${this.base}/workouts`, workout, { params: { workoutPlanId: planId } });
  }

  updateWorkout(id: number, planId: number, workout: Partial<Workout>): Observable<number> {
    return this.http.put<number>(`${this.base}/workouts/${id}`, { workoutPlanId: planId, ...workout });
  }

  getExerciseDefinitions(userId: string): Observable<ExerciseDefinition[]> {
    return this.http.get<ExerciseDefinition[]>(`${this.base}/exercise-definitions`, { params: { userId } });
  }

  createExercise(userId: string, exercise: Partial<ExerciseDefinition>): Observable<number> {
    return this.http.post<number>(`${this.base}/exercises`, exercise, { params: { userId } });
  }

  updateExercise(id: number, exercise: Partial<ExerciseDefinition>): Observable<number> {
    return this.http.put<number>(`${this.base}/exercises/${id}`, exercise);
  }

  getHistory(userId: string): Observable<WorkoutHistoryItem[]> {
    return this.http.get<WorkoutHistoryItem[]>(`${this.base}/history`, { params: { userId } });
  }
}
