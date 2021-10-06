using System.Collections.Generic;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        public IReadOnlyCollection<TaskDTO> All()
        {
            throw new System.NotImplementedException();
        }

        public int Create(TaskDTO task)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TaskDTO task)
        {
            throw new System.NotImplementedException();
        }
    }
}
