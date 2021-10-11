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
            var tasks = from t in _context.Tasks
                        where t.State == state
                        select new TaskDTO(
                            t.ID,
                            t.Title,
                            t.AssignedTo.Name,
                            t.Tags.Select(t => t.Name).ToList().AsReadOnly(),
                            t.State
                            );

            return tasks.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            var tasks = from t in _context.Tasks
                        where t.Tags.Contains(GetTag(tag))
                        select new TaskDTO(
                            t.ID,
                            t.Title,
                            t.AssignedTo.Name,
                            t.Tags.Select(t => t.Name).ToList().AsReadOnly(),
                            t.State
                            );

            return tasks.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            var tasks = from c in _context.Tasks
                        where c.AssignedTo.ID == userId
                        select new TaskDTO(
                            c.ID,
                            c.Title,
                            c.AssignedTo.Name,
                            c.Tags.Select(t => t.Name).ToList().AsReadOnly(),
                            c.State
                        );

            return tasks.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            return ReadAllByState(State.Removed);
        }

        public Response Update(TaskUpdateDTO task)
        {
            var taskToBeUpdated = GetTask(task.Id);

            if (taskToBeUpdated == null)
            {
                return Response.NotFound;
            }
            else
            {
                var (response, user) = GetUser(task.AssignedToId);
                if (response == Response.BadRequest)
                {
                    return (Response.BadRequest);
                }
                taskToBeUpdated.AssignedTo = user;
                taskToBeUpdated.Description = task.Description;
                taskToBeUpdated.State = task.State;
                taskToBeUpdated.Tags = GetTags(task.Tags);
                taskToBeUpdated.Title = task.Title;
                taskToBeUpdated.StateUpdated = DateTime.Now;

                _context.Tasks.Update(taskToBeUpdated);
                _context.SaveChanges();

                return Response.Updated;
            }
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

        private Tag GetTag(string tagName)
        {
            return _context.Tags.Where(t => t.Name == tagName).FirstOrDefault();
        }
    }

}
