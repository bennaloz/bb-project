using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Client.Data
{
    public class BBProjectDatabase
    {
        public const string dbFileName = "bbProject.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, dbFileName);
            }
        }

        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<BBProjectDatabase> Instance = new AsyncLazy<BBProjectDatabase>(async () =>
        {
            var instance = new BBProjectDatabase();
            CreateTableResult result = await Database.CreateTableAsync<WorkoutSession>();
            result = await Database.CreateTableAsync<SessionExercise>();
            return instance;
        });

        public BBProjectDatabase()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
        }

        public async Task<WorkoutSession> GetWorkoutSessionAsync()
            => await Database.Table<WorkoutSession>().FirstOrDefaultAsync();

        public async Task SaveWorkoutSessionAsync(WorkoutSession workoutSession)
        {
            if(workoutSession.Id != 0)
            {
                await Database.UpdateAsync(workoutSession);
            }
            else
            {
                workoutSession.Id = await Database.InsertAsync(workoutSession);
            }
        }

        public async Task DeleteWorkoutSessionAsync(WorkoutSession workoutSession)
        {
            await Database.DeleteAsync(workoutSession);
            await Database.Table<SessionExercise>().DeleteAsync();
        }
         
        public async Task SaveSessionExerciseAsync(SessionExercise exercise)
        {
            if (exercise.Id != 0)
                await Database.UpdateAsync(exercise);
            else
                exercise.Id = await Database.InsertAsync(exercise);
        }

        public async Task<IEnumerable<SessionExercise>> GetSessionExercisesAsync()
            => await Database.Table<SessionExercise>().ToListAsync();

        public async Task<SessionExercise> GetSessionExerciseAsync(ulong exerciseId)
            => (await Database.QueryAsync<SessionExercise>(
                                $"SELECT * " +
                                $"FROM [{nameof(SessionExercise)}] " +
                                $"  WHERE [{nameof(SessionExercise.ExerciseId)}] = {exerciseId}")
                ).FirstOrDefault();
    }
}
