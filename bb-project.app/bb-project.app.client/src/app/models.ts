export interface WorkoutPlan {
  id: number;
  name: string;
  isActive: boolean;
}

export interface Workout {
  id: number;
  name: string;
  order: number;
}

export interface ExerciseDefinition {
  id: number;
  name: string;
  type: ExerciseType;
  involvedMuscles: InvolvedMuscles;
}

export enum ExerciseType {
  Cardio = 0,
  Weights = 1
}

export enum InvolvedMuscles {
  Pectorals  = 0x001,
  Back       = 0x002,
  Deltoids   = 0x004,
  Quadriceps = 0x008,
  Hamstrings = 0x010,
  Calf       = 0x020,
  Biceps     = 0x040,
  Triceps    = 0x080,
  Heart      = 0x100
}

export interface WorkoutHistoryItem {
  startDate: string;
  endDate: string;
  workoutId: number;
  userId: string;
}
