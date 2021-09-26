using Assignment4.Entities;
using System;
using System.Collections.Generic;

namespace Assignment4.Model
{
    public class TaskRepository : IDisposable
    {
        private readonly KanbanContext _context;

        public TaskRepository(KanbanContext context)
        {
            _context = context;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        /// <returns>The id of the newly created task</returns>
        public int Create(TaskDTO task)
        {
            throw new NotImplementedException();
        }

        public TaskDetailsDTO FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> All()
        {
            throw new NotImplementedException();
        }

        public void Update(TaskDTO task)
        {
            throw new NotImplementedException();
        }

        public void Delete(int taskId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
