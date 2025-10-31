using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public IEnumerable<TaskItem> GetAll() => _tasks;
        public TaskItem Add(TaskItem task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
            return task;
        }
        public TaskItem? Toggle(int id)
        {
            var t = _tasks.FirstOrDefault(x => x.Id == id);
            if (t == null) return null;
            t.IsCompleted = !t.IsCompleted;
            return t;
        }
        public bool Delete(int id)
        {
            var t = _tasks.FirstOrDefault(x => x.Id == id);
            return t != null && _tasks.Remove(t);
        }
    }
}
