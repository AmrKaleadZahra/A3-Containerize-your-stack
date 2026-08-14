using A2_Connecting_to_the_database.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace A2_Connecting_to_the_database.Services
{

    public class TaskService :ITaskService
    {
        private readonly TasksDbContext _dbContext;

        public TaskService(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TbTask> GetTaskById(int id)
        {
            
            TbTask? task = await _dbContext.Tasks.FirstOrDefaultAsync(t=> t.Id == id);
            if(task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            return task;
        }
        
        public async Task<List<TbTask>> GetTasks()
        {
            var task = await _dbContext.Tasks.ToListAsync();
            if (task == null)
                throw new KeyNotFoundException("No Tasks Founded");
            return task;
        }

        public async Task<List<TbTask>> IsDoneSearch(bool? done,string? search)
        {
            var query = _dbContext.Tasks.AsQueryable();
            if (done.HasValue)
            {
                query = query.Where(t => t.Done == done.Value);
                
            }
            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Title.Contains(search));
                
            }

            var tasks = await query.ToListAsync();
            if (!tasks.Any())
            {
                throw new KeyNotFoundException($"No tasks found with search term");
            }
            return tasks;
        }

        public async Task<int> tasksCount()
        {
            return await _dbContext.Tasks.CountAsync();
        }

        public async Task<int> DoneCount()
        {
            return await _dbContext.Tasks.CountAsync(t => t.Done);
        }

        public async Task<int> PendingCount()
        {
            return await _dbContext.Tasks.CountAsync(t => !t.Done);
        }

        public async Task<TbTask> CreateTask(CreateTaskRequest taskRequest)
        {
            if(taskRequest == null)
                throw new ArgumentNullException(nameof(taskRequest), "Task request cannot be null.");
            if (string.IsNullOrWhiteSpace(taskRequest.Title))
                throw new ArgumentException("Title is Required", nameof(taskRequest.Title));
            TbTask newTask = new TbTask
            {
                Title = taskRequest.Title,
            };
            _dbContext.Tasks.Add(newTask);
            return await _dbContext.SaveChangesAsync() > 0?newTask: throw new Exception("Failed to create task.");
            
        }

        public async Task<bool> UpdateTask(int id, UpdatedTask task)
        {
            if(task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            var existingTask = await _dbContext.Tasks.FindAsync(id);
            if(existingTask == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            if(string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentNullException(nameof(task.Title), "Title is Required");
            if(existingTask.Title == task.Title && existingTask.Done == task.Done)
                throw new InvalidOperationException("No changes detected.");
            _dbContext.Entry(existingTask).CurrentValues.SetValues(task);
            existingTask.Title = task.Title;
            existingTask.Done = task.Done;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskById(int id)
        {
            var exisyingTask = await _dbContext.Tasks.FindAsync(id);
            if(exisyingTask == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            _dbContext.Tasks.Remove(exisyingTask);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Reset()
        {
            _dbContext.Tasks.RemoveRange(_dbContext.Tasks);
            var result = await _dbContext.SaveChangesAsync() > 0;
            if (!result)
                return false;
            await SeedData.SeedAsync(_dbContext);
            return true;
        }
    }
}
