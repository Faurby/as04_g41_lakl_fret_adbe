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
    public class UserRepositoryTest : IDisposable
    {

        private readonly IKanbanContext _context;
        private readonly UserRepository _repo;

        public UserRepositoryTest()
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

            var user3 = new User()
            {
                ID = 3,
                Email = "ronweasly@gmail.com",
                Name = "Ron Weasly"
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
            context.Users.Add(user3);
            context.SaveChanges();

            // Set context and create Repository with context
            _context = context;
            _repo = new UserRepository(_context);
        }

        [Fact]
        public void Create_creates_new_User_with_generated_id()
        {
            // Arrange
            var user = new UserCreateDTO()
            {
                Email = "HarryPotter@hotmail.com",
                Name = "Harry Potter"

            };

            var created = _repo.Create(user);

            Assert.Equal(4, created.UserId);
            Assert.Equal(Response.Created, created.Response);
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
        public void Delete_With_Force_Deletes_User_With_Tasks()
        {
            //Given
            var expected = Response.Deleted;
            var actual = _repo.Delete(1, true);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Read_UserId_Returns_UserDTO()
        {
            //Given
            var expected = new UserDTO(1, "Anton", "anton@email.com");
            var actual = _repo.Read(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAll_Returns_List_Of_Users()
        {
            //Given
            var expected = 3;
            var actual = _repo.ReadAll().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update_Should_Return_Response_Updated()
        {
            //Given
            var expected = Response.Updated;
            var userToUpdate = new UserUpdateDTO() { Id = 1, Email = "anton@gmail.com", Name = "Anton Klammenborg" };
            var actual = _repo.Update(userToUpdate);

            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
