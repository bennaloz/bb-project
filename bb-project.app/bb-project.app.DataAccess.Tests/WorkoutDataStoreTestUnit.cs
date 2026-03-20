using bb_project.app.Contracts.Interfaces;
using bb_project.app.Contracts.Models.Data;
using bb_project.app.Contracts.Models.Enums;
using bb_project.Infrastructure.BLL;

namespace bb_project.Server.Tests
{
    [TestClass]
    [TestCategory("Integration")]
    public class WorkoutDataStoreTestUnit
    {
        private readonly IWorkoutsDataStore workoutsDataStore = new WorkoutsDataStore();
        private readonly string testUnitUserId = "{9EA8FA7C-C099-42F9-9B8A-3B400D1B82C8}";

        private ExerciseDefinition pancaPiana;
        private ExerciseDefinition pancaInclManubri;
        private ExerciseDefinition curlBicAlternati;
        private ExerciseDefinition crociCaviBasso;
        private ExerciseDefinition arnoldPress;
        private ExerciseDefinition alzateLaterali;
        private ExerciseDefinition hummerCurl;

        public WorkoutDataStoreTestUnit()
        {
            this.pancaPiana = new ExerciseDefinition()
            {
                Name = "Panca piana",
                Type = ExerciseType.Weights,
                InvolvedMuscles = InvolvedMuscles.Pectorals | InvolvedMuscles.Deltoids | InvolvedMuscles.Triceps
            };
            this.pancaInclManubri = new ExerciseDefinition
            {
                Name = "Panca inclinata manubri",
                Type = ExerciseType.Weights,
                InvolvedMuscles = InvolvedMuscles.Pectorals | InvolvedMuscles.Deltoids | InvolvedMuscles.Triceps
            };
            this.curlBicAlternati = new ExerciseDefinition
            {
                Name = "Curl bicipiti alternati in piedi",
                Type = ExerciseType.Weights,
                InvolvedMuscles = InvolvedMuscles.Biceps
            };
            this.crociCaviBasso = new ExerciseDefinition
            {
                Name = "Croci cavi dal basso",
                Type = ExerciseType.Weights,
                InvolvedMuscles = InvolvedMuscles.Pectorals
            };
            this.arnoldPress = new ExerciseDefinition
            {
                Name = "Arnold press",
                InvolvedMuscles = InvolvedMuscles.Deltoids | InvolvedMuscles.Triceps,
                Type = ExerciseType.Weights
            };
            this.alzateLaterali = new ExerciseDefinition
            {
                Name = "Alzate laterali",
                InvolvedMuscles = InvolvedMuscles.Deltoids,
                Type = ExerciseType.Weights
            };
            this.hummerCurl = new ExerciseDefinition
            {
                Name = "Hummer curl",
                InvolvedMuscles = InvolvedMuscles.Biceps,
                Type = ExerciseType.Weights
            };
        }

        [TestMethod]
        public async Task InsertExerciseDefinitionAsyncTest()
        {
            try
            {
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.pancaPiana);
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.pancaInclManubri);
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.curlBicAlternati);
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.crociCaviBasso);
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.arnoldPress);
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.alzateLaterali);
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, this.hummerCurl);

                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, new ExerciseDefinition()
                {
                    Name = "Squat",
                    Type = ExerciseType.Weights,
                    InvolvedMuscles = InvolvedMuscles.Quadriceps | InvolvedMuscles.Hamstrings
                });

            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task InsertWorkoutPlanAsyncTest()
        {
            try
            {
                var woPlanName = "JumpSetCut";
                var woPlanId = await this.workoutsDataStore.InsertWorkoutPlanAsync(this.testUnitUserId, woPlanName, isActive: true);
                Assert.IsTrue(woPlanId > 0);
                var woPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync(woPlanId)).First();
                Assert.AreEqual(woPlan.Name, woPlanName);
                Assert.IsTrue(woPlan.IsActive);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task InsertWorkoutAsyncTest()
        {
            try
            {
                var woName = "Petto";
                var woPlanId = (await this.workoutsDataStore.GetWorkoutPlansAsync()).First().Id;
                var woId = await this.workoutsDataStore.InsertWorkoutAsync(woPlanId, woName, 1);
                Assert.IsTrue(woId > 0);

                var wo = (await this.workoutsDataStore.GetWorkoutsAsync(woPlanId, workoutId: woId)).First();
                Assert.IsNotNull(wo);
                Assert.AreEqual(wo.Name, woName);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        private async Task loadExercises()
        {
            var exercises = await this.workoutsDataStore.GetExerciseDefinitionsAsync(this.testUnitUserId);
            this.pancaPiana = exercises.First(e => e.Name == this.pancaPiana.Name);
            this.pancaInclManubri = exercises.First(e => e.Name == this.pancaInclManubri.Name);
            this.arnoldPress = exercises.First(e => e.Name == this.arnoldPress.Name);
            this.curlBicAlternati = exercises.First(e => e.Name == this.curlBicAlternati.Name);
            this.hummerCurl = exercises.First(e => e.Name == this.hummerCurl.Name);
            this.crociCaviBasso = exercises.First(e => e.Name == this.crociCaviBasso.Name);
            this.alzateLaterali = exercises.First(e => e.Name == this.alzateLaterali.Name);
        }

        [TestMethod]
        public async Task InsertWorkoutSeriesAsyncTest()
        {
            try
            {
                await this.loadExercises();

                var woPlanId = (await this.workoutsDataStore.GetWorkoutPlansAsync()).First().Id;
                var woId = (await this.workoutsDataStore.GetWorkoutsAsync(woPlanId)).First().Id;

                var seriesGroups = new List<ExerciseGroup>();
                var panca = new ExerciseGroup(ExerciseMethodology.Single);
                panca.Exercises.Add(0, new Exercise(this.pancaPiana));
                panca.Exercises[0].Series.Add(new Serie()
                {
                    Reps = 4,
                    Rest = TimeSpan.FromSeconds(120)
                });
                panca.Exercises[0].Series.Add(new Serie()
                {
                    Reps = 4,
                    Rest = TimeSpan.FromSeconds(120)
                });
                panca.Exercises[0].Series.Add(new Serie()
                {
                    Reps = 6,
                    Rest = TimeSpan.FromSeconds(120)
                });
                panca.Exercises[0].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(90)
                });
                panca.Exercises[0].Series.Add(new Serie()
                {
                    Reps = 10,
                    Rest = TimeSpan.FromSeconds(300)
                });
                seriesGroups.Add(panca);

                var jumpset1 = new ExerciseGroup(ExerciseMethodology.JumpSet);
                jumpset1.Exercises.Add(1, new Exercise(this.pancaInclManubri));
                jumpset1.Exercises.Add(2, new Exercise(this.curlBicAlternati));
                jumpset1.Exercises[1].Series.Add(new Serie()
                {
                    Reps = 10,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[2].Series.Add(new Serie()
                {
                    Reps = 6,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[1].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[2].Series.Add(new Serie()
                {
                    Reps = 6,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[1].Series.Add(new Serie()
                {
                    Reps = 6,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[2].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[1].Series.Add(new Serie()
                {
                    Reps = 6,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset1.Exercises[2].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(60)
                });
                seriesGroups.Add(jumpset1);

                var crociCavi = new ExerciseGroup(ExerciseMethodology.Single);
                crociCavi.Exercises.Add(3, new Exercise(this.crociCaviBasso));
                crociCavi.Exercises[3].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                crociCavi.Exercises[3].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                crociCavi.Exercises[3].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                crociCavi.Exercises[3].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                seriesGroups.Add(crociCavi);

                var jumpset2 = new ExerciseGroup(ExerciseMethodology.JumpSet);
                jumpset2.Exercises.Add(4, new Exercise(this.arnoldPress));
                jumpset2.Exercises.Add(5, new Exercise(this.alzateLaterali));
                jumpset2.Exercises[4].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset2.Exercises[5].Series.Add(new Serie()
                {
                    Reps = 12,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset2.Exercises[4].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset2.Exercises[5].Series.Add(new Serie()
                {
                    Reps = 12,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset2.Exercises[4].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(60)
                });
                jumpset2.Exercises[5].Series.Add(new Serie()
                {
                    Reps = 12,
                    Rest = TimeSpan.FromSeconds(60)
                });
                seriesGroups.Add(jumpset2);

                var hummerCurl = new ExerciseGroup(ExerciseMethodology.Single);
                hummerCurl.Exercises.Add(6, new Exercise(this.hummerCurl));
                hummerCurl.Exercises[6].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                hummerCurl.Exercises[6].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                hummerCurl.Exercises[6].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                hummerCurl.Exercises[6].Series.Add(new Serie()
                {
                    Reps = 8,
                    Rest = TimeSpan.FromSeconds(75)
                });
                seriesGroups.Add(hummerCurl);

                await this.workoutsDataStore.InsertSeriesGroupsAsync(woId, seriesGroups);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task InsertWorkoutHistoryTestAsync()
        {
            try
            {

                var woPlanId = (await this.workoutsDataStore.GetWorkoutPlansAsync()).First().Id;
                var woId = (await this.workoutsDataStore.GetWorkoutsAsync(woPlanId)).First().Id;

                var workoutHistoryId = await this.workoutsDataStore.InsertWorkoutHistoryAsync(DateTime.Now, DateTime.Now.AddHours(1), woId, woPlanId, this.testUnitUserId);

                var woExerciseGroups = await this.workoutsDataStore.GetWorkoutExercisesAsync(woId, this.testUnitUserId);

                DateTime startTime = DateTime.Now;

                foreach (var woExerciseGroup in woExerciseGroups)
                {
                    foreach (var exercise in woExerciseGroup.Exercises.Values)
                    {
                        foreach (var serie in exercise.Series)
                        {
                            await this.workoutsDataStore.InsertWorkoutDataAsync(workoutHistoryId, serie.Id, exercise.Id,
                                startTime = startTime.AddMinutes(1),
                                startTime.AddMinutes(1),
                                120.5
                            );
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task UpdateExerciseDefinitionTestAsync()
        {
            try
            {
                await this.loadExercises();
                var modifiedExercisesCount = await this.workoutsDataStore.UpdateExerciseDefinitionAsync(this.pancaPiana.Id, $"{this.pancaPiana.Name}_2", ExerciseType.Cardio, InvolvedMuscles.Hamstrings);

                Assert.IsTrue(modifiedExercisesCount == 1);
                var pancaPianaModified = (await this.workoutsDataStore.GetExerciseDefinitionsAsync(this.testUnitUserId)).FirstOrDefault(e=>e.Id == this.pancaPiana.Id);
                Assert.IsNotNull(pancaPianaModified);
                Assert.IsTrue(pancaPianaModified.InvolvedMuscles == InvolvedMuscles.Hamstrings);
                Assert.IsTrue(pancaPianaModified.Type == ExerciseType.Cardio);
                Assert.IsTrue(pancaPianaModified.Name == $"{this.pancaPiana.Name}_2");

                modifiedExercisesCount = await this.workoutsDataStore.UpdateExerciseDefinitionAsync(this.pancaPiana.Id, this.pancaPiana.Name, this.pancaPiana.Type, this.pancaPiana.InvolvedMuscles);
                Assert.IsTrue(modifiedExercisesCount == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task UpdateWorkoutPlanTestAsync()
        {
            try
            {
                var woPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync()).First();
                Assert.IsNotNull(woPlan);

                var updatedWorkoutPlansCount = await this.workoutsDataStore.UpdateWorkoutPlanAsync(woPlan.Id, this.testUnitUserId, $"{woPlan.Name}_2", false, false);
                Assert.IsTrue(updatedWorkoutPlansCount == 1);

                var modifiedWoPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync(id: woPlan.Id)).First();
                Assert.AreNotEqual(woPlan.Name, modifiedWoPlan.Name);
                Assert.IsTrue(modifiedWoPlan.Name == $"{woPlan.Name}_2");
                Assert.IsFalse(modifiedWoPlan.IsActive);

                updatedWorkoutPlansCount = await this.workoutsDataStore.UpdateWorkoutPlanAsync(woPlan.Id, this.testUnitUserId, woPlan.Name, woPlan.IsActive, true);
                Assert.IsTrue(updatedWorkoutPlansCount == 1);

                modifiedWoPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync(woPlan.Id)).FirstOrDefault();
                Assert.IsNull(modifiedWoPlan);

                modifiedWoPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync(woPlan.Id, true)).FirstOrDefault();
                Assert.IsNotNull(modifiedWoPlan);

                updatedWorkoutPlansCount = await this.workoutsDataStore.UpdateWorkoutPlanAsync(woPlan.Id, this.testUnitUserId, woPlan.Name, woPlan.IsActive, false);
                Assert.IsTrue(updatedWorkoutPlansCount == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task UpdateWorkoutTestAsync()
        {
            try
            {
                var woPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync()).First();
                var wo = (await this.workoutsDataStore.GetWorkoutsAsync(woPlan.Id)).First();

                var updatedWorkoutRecordsCount = await this.workoutsDataStore.UpdateWorkoutAsync(woPlan.Id, wo.Id, $"{wo.Name}_2", wo.Order + 1);
                Assert.IsTrue(updatedWorkoutRecordsCount == 1);

                var modifiedWo = (await this.workoutsDataStore.GetWorkoutsAsync(woPlan.Id, wo.Id)).First();
                Assert.IsNotNull(modifiedWo);
                Assert.IsTrue(modifiedWo.Name == $"{wo.Name}_2");
                Assert.IsTrue(modifiedWo.Order == wo.Order + 1);

                updatedWorkoutRecordsCount = await this.workoutsDataStore.UpdateWorkoutAsync(woPlan.Id, wo.Id, wo.Name, wo.Order);
                Assert.IsTrue(updatedWorkoutRecordsCount == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task DeleteWorkoutSeriesTestAsync()
        {
            try
            {
                const int seriesCountToDelete = 2;

                async Task<ulong[]> GetSeriesIdsToDeleteAsync(ulong workoutId)
                {
                    return (await this.workoutsDataStore.GetWorkoutExercisesAsync(workoutId, this.testUnitUserId))
                        .SelectMany(g => g.Exercises.Values)
                        .SelectMany(e => e.Series)
                        .Select(s => s.Id)
                        .Where(id => id > 0)
                        .Distinct()
                        .Take(seriesCountToDelete)
                        .ToArray();
                }

                var woPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync()).FirstOrDefault();
                if (woPlan == null)
                {
                    var woPlanId = await this.workoutsDataStore.InsertWorkoutPlanAsync(this.testUnitUserId, "DeleteSeriesTestPlan", isActive: true);
                    woPlan = (await this.workoutsDataStore.GetWorkoutPlansAsync(id: woPlanId)).First();
                }

                var wo = (await this.workoutsDataStore.GetWorkoutsAsync(woPlan.Id)).FirstOrDefault();
                if (wo == null)
                {
                    var woId = await this.workoutsDataStore.InsertWorkoutAsync(woPlan.Id, "DeleteSeriesTestWorkout", 1);
                    wo = (await this.workoutsDataStore.GetWorkoutsAsync(woPlan.Id, woId)).First();
                }

                var seriesIdsToDelete = await GetSeriesIdsToDeleteAsync(wo.Id);

                if (!seriesIdsToDelete.Any())
                {
                    var exerciseDefinition = (await this.workoutsDataStore.GetExerciseDefinitionsAsync(this.testUnitUserId)).FirstOrDefault();
                    if (exerciseDefinition == null)
                    {
                        await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, new ExerciseDefinition
                        {
                            Name = "DeleteSeriesTestExercise",
                            Type = ExerciseType.Weights,
                            InvolvedMuscles = InvolvedMuscles.Biceps
                        });
                        exerciseDefinition = (await this.workoutsDataStore.GetExerciseDefinitionsAsync(this.testUnitUserId)).First();
                    }

                    var exerciseGroup = new ExerciseGroup(ExerciseMethodology.Single);
                    exerciseGroup.Exercises.Add(exerciseDefinition.Id, new Exercise(exerciseDefinition));
                    exerciseGroup.Exercises[exerciseDefinition.Id].Series.Add(new Serie { Reps = 10, Rest = TimeSpan.FromSeconds(60) });
                    exerciseGroup.Exercises[exerciseDefinition.Id].Series.Add(new Serie { Reps = 8, Rest = TimeSpan.FromSeconds(60) });
                    await this.workoutsDataStore.InsertSeriesGroupsAsync(wo.Id, new[] { exerciseGroup });

                    seriesIdsToDelete = await GetSeriesIdsToDeleteAsync(wo.Id);
                }

                Assert.IsTrue(seriesIdsToDelete.Length > 0, "No series IDs were found or created for deletion test.");

                var deletedSeriesCount = await this.workoutsDataStore.DeleteWorkoutSeriesAsync(woPlan.Id, wo.Id, seriesIdsToDelete);
                Assert.IsTrue(deletedSeriesCount > 0, "Expected at least one series to be deleted.");

                var remainingSeriesIds = (await this.workoutsDataStore.GetWorkoutExercisesAsync(wo.Id, this.testUnitUserId))
                    .SelectMany(g => g.Exercises.Values)
                    .SelectMany(e => e.Series)
                    .Select(s => s.Id)
                    .ToHashSet();

                foreach (var seriesId in seriesIdsToDelete)
                {
                    Assert.IsFalse(remainingSeriesIds.Contains(seriesId), $"Series ID {seriesId} should have been deleted but was found in remaining series.");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
