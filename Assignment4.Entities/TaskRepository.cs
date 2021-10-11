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
using System;

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
            var (response, user) = GetUser(task.AssignedToId);
            if (response == Response.BadRequest)
            {
                return (Response.BadRequest, -1);
            }

            var entity = new Task
            {
                AssignedTo = user,
                Description = task.Description,
                State = State.New,
                Tags = GetTags(task.Tags),
                Title = task.Title,
                Created = DateTime.Now,
                StateUpdated = DateTime.Now
            };

            _context.Tasks.Add(entity);
            _context.SaveChanges();

            return (Response.Created, entity.ID);
        }

        public Response Delete(int taskId)
        {
            var taskToBeDeleted = GetTask(taskId);

            if (taskToBeDeleted == null)
            {
                return Response.NotFound;
            }
            else
            {
                if (taskToBeDeleted.State == State.Active)
                {
                    taskToBeDeleted.State = State.Removed;
                    _context.Tasks.Update(taskToBeDeleted);
                    _context.SaveChanges();
                    return Response.Deleted;
                }
                else
                {
                    return Response.Conflict;
                }
            }
        
        }

        public TaskDetailsDTO Read(int taskId)
        {
            var tasks = from c in _context.Tasks
                        where c.ID == taskId
                        select new TaskDetailsDTO(
                            c.ID,
                            c.Title,
                            c.Description,
                            c.Created,
                            c.AssignedTo.Name,
                            c.Tags.Select(t => t.Name).ToList().AsReadOnly(),
                            c.State,
                            c.StateUpdated
                        );

            return tasks.FirstOrDefault();
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

        private (Response, User) GetUser(int? id)
        {
            if (id != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    return (Response.BadRequest, null);
                }
                else
                {
                    return (Response.Found, user);
                }
            }
            else
            {
                return (Response.NotFound, null);
            }
        }

        private Task GetTask(int taskId)
        {
            return _context.Tasks.Find(taskId);
        }

        private ICollection<Tag> GetTags(ICollection<string> tags)
        {
            var list = new List<Tag>();
            foreach (var tag in tags)
            {
                list.Add(_context.Tags.FirstOrDefault(t => t.Name == tag));
            }
            return list;
        }
    }

}
