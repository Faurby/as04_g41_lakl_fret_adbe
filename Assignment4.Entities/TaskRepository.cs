using System.Collections.Generic;
using System.IO;
using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Assignment4;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using Lecture05.Entities;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {

        private readonly IKanbanContext _context;

        public TaskRepository(IKanbanContext context)
        {
            _context = context;
        }
        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            var entity = new Task
            {
                AssignedTo = GetUser(task.AssignedToId),
                Description = task.Description,
                State = State.New,
                
            }
        }

        public Response Delete(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO Read(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            return _context.Tasks.Select(t => new TaskDTO(
                t.ID,
                t.Title,
                t.AssignedTo.Name,
                t.Tags.Select(s => s.Name).ToList(),
                t.State))
                .ToList().AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            throw new System.NotImplementedException();
        }

        public Response Update(TaskUpdateDTO task)
        {
            throw new System.NotImplementedException();
        }

        private User GetUser(int? id)
        {
            return _context.Users.FirstOrDefault(u => u.ID == id);
        }
    }

}
