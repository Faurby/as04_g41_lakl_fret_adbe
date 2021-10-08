using System.Collections.Generic;
using System.IO;
using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Assignment4;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Immutable;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
    }

}
