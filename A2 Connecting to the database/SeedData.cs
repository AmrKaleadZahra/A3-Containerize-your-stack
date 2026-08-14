using A2_Connecting_to_the_database;

namespace A2_Connecting_to_the_database
{
    public class SeedData
    {
        public static async Task SeedAsync(TasksDbContext context)
        {
            if (!context.Tasks.Any())
            {
                var tasks = new List<TbTask>
                {
                    new TbTask { Title = "Task 1", Done = false },
                    new TbTask { Title = "Task 2", Done = true },
                    new TbTask { Title = "Task 3", Done = false }
                };
                context.Tasks.AddRange(tasks);
                await context.SaveChangesAsync();
            }
        }
    }
}
