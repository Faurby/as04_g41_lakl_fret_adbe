using System;
using Xunit;
using System.Collections.Generic;
using Assignment4.Core;
using System.IO;
using Assignment4.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.Linq;
using Lecture05.Entities;
using Microsoft.Data.Sqlite;
using System.Reflection;


namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests : IDisposable
    {
        private readonly IKanbanContext _context;
        private readonly TaskRepository _repo;

        public TaskRepositoryTests()
        {
            // Make SQLite server in memory
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var build = new DbContextOptionsBuilder<KanbanContext>();
            build.UseSqlite(connection);
            var context = new KanbanContext(build.Options);
            context.Database.EnsureCreated();

            // Create temp data
            var user1 = new User()
            {
                ID = 1,
                Email = "anton@email.com",
                Name = "Anton"
            };
            var user2 = new User()
            {
                ID = 2,
                Email = "lasse@email.com",
                Name = "Lasse"
            };

            var tag1 = new Tag()
            {
                ID = 1,
                Name = "Frontend"
            };
            var tag2 = new Tag()
            {
                ID = 2,
                Name = "Backend"
            };

            var task1 = new Task()
            {
                AssignedTo = user1,
                // ID = 1,
                Title = "Add button",
                Description = "Adds a button",
                State = State.New,
                Tags = new[] { tag1 },
                Created = DateTime.Now,
                StateUpdated = DateTime.Now
            };

            var task2 = new Task()
            {
                AssignedTo = user1,
                // ID = 2,
                Title = "Remove button",
                Description = "Removes a button",
                State = State.New,
                Tags = new[] { tag1 },
                Created = DateTime.Now,
                StateUpdated = DateTime.Now
            };

            var task3 = new Task()
            {
                AssignedTo = user2,
                // ID = 3,
                Title = "Optimize algorithm",
                Description = "Optimizes algorithms",
                State = State.Active,
                Tags = new[] { tag2 },
                Created = DateTime.Now,
                StateUpdated = DateTime.Now
            };

            context.Tasks.AddRange(task1, task2, task3);
            context.SaveChanges();

            // Set context and create Repository with context
            _context = context;
            _repo = new TaskRepository(_context);
        }

        // TODO: Find out how to compare 2 lists in records?
        [Fact]
        public void ReadAll_Returns_All_Tasks()
        {
            // Arrange
            var tasks = _repo.ReadAll();

            var expected = _context.Tasks.Count();
            var actual = tasks.Count();

            Assert.Equal(expected, actual);


            // Assert.Collection(tasks,
            //     t => Assert.Equal(new TaskDTO(1, "Add button", "Anton", null, State.New), t),
            //     t => Assert.Equal(new TaskDTO(2, "Remove button", "Anton", new List<string>{ "frontend" }, State.New), t),
            //     t => Assert.Equal(new TaskDTO(3, "Optimize algorithm", "Lasse", new List<string> { "backend" }, State.New), t)
            // );
        }

        [Fact]
        public void Create_creates_new_Task_with_generated_id()
        {
            // Arrange
            var task = new TaskCreateDTO()
            {
                AssignedToId = 1,
                Title = "Analyse buttons",
                Description = "Analyses stuff",
                Tags = new[] { "Frontend" }
            };

            var created = _repo.Create(task);

            Assert.Equal(4, created.TaskId);
            Assert.Equal(Response.Created, created.Response);
        }

        [Fact]
        public void Read_Returns_TaskDetailsDTO_from_ID()
        {
            //Given
            var time = DateTime.Now;
            var expected = new TaskDetailsDTO(
                1,
                "Add button",
                "Adds a button",
                time,
                "Anton",
                new[] { "Frontend" },
                State.New,
                DateTime.Now
            );

            //When
            var actual = _repo.Read(1);


            //Then
            Assert.True(expected.TestEquals(actual));
        }

        [Fact]
        public void Delete_Returns_Proper_Response()
        {
            //Given
            var expected = Response.Deleted;
            var actual = _repo.Delete(3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_Changes_State_To_Removed()
        {
            _repo.Delete(3);

            var expected = State.Removed;

            var actual = _context.Tasks.Find(3).State;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAllByState_Returns_List_Of_Tasks_By_State()
        {
            //Given
            var expected = new List<int> { 1, 2 };
            var actual = _repo.ReadAllByState(State.New);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected.ElementAt(i), actual.ElementAt(i).Id);
            }
        }

        [Fact]
        public void ReadAllByTag_Returns_List_Of_Tasks_By_Tag()
        {
            //Given
            var expected = new List<int> { 1, 2 };
            var actual = _repo.ReadAllByTag("Frontend");

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected.ElementAt(i), actual.ElementAt(i).Id);
            }
        }

        [Fact]
        public void ReadAllRemoved_Returns_List_Of_Removed_Tasks()
        {
            //Given
            var expected = new List<int>(3);

            _repo.Delete(3);
            var actual = _repo.ReadAllRemoved();

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected.ElementAt(i), actual.ElementAt(i).Id);
            }
        }

        [Fact]
        public void Update_Changes_Title()
        {
            _repo.Update(new TaskUpdateDTO() { Id = 1, AssignedToId = 1, Description = "Adds a button", State = State.Active, Title = "Add another button", Tags = new[] { "Frontend" } });

            var expected = "Add another button";

            var actual = _context.Tasks.Find(1).Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAllByUser_given_id_1_returns_add_button_and_remove_button()
        {
            var tasks = _repo.ReadAllByUser(1);

            var expected = 2;
            var actual = tasks.Count();

            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
