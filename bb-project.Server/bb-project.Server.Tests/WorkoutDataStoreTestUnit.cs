using bb_project.Infrastructure.BLL;
using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Server.Tests
{
    [TestClass]
    public class WorkoutDataStoreTestUnit
    {
        private readonly WorkoutsDataStore workoutsDataStore = new WorkoutsDataStore();
        private readonly string testUnitUserId = "{9EA8FA7C-C099-42F9-9B8A-3B400D1B82C8}";

        [TestMethod]
        public async Task InsertExerciseDefinitionAsyncTest()
        {
            try
            {
                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, new ExerciseDefinition()
                {
                    Name = "Panca piana",
                    Type = ExerciseType.Weights
                });

                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, new ExerciseDefinition()
                {
                    Name = "Squat",
                    Type = ExerciseType.Weights
                });

                await this.workoutsDataStore.InsertExerciseDefinitionAsync(this.testUnitUserId, new ExerciseDefinition()
                {
                    Name = "Distensioni manubri panca inclinata",
                    Type = ExerciseType.Weights
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
    }
}