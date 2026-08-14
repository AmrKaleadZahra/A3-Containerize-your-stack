using A2_Connecting_to_the_database.DTOs;

namespace A2_Connecting_to_the_database
{
    public interface ITaskService
    {
        Task<List<TbTask>> GetTasks();
        Task<TbTask> GetTaskById(int id);
        Task<List<TbTask>> IsDoneSearch(bool? done, string? search);
        Task<int> tasksCount();
        Task<int> DoneCount();
        Task<int> PendingCount();
        Task<TbTask> CreateTask(CreateTaskRequest taskRequest);
        Task<bool> UpdateTask(int id, UpdatedTask task);
        Task<bool> DeleteTaskById(int id);
        Task<bool> Reset();
    }
}
