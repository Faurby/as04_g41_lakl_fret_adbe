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

namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests
    {
        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<TaskRepositoryTests>();

            return builder.Build();
        }

        [Fact]
        public void All_Returns_All_Tasks()
        {
            // var col = new List<TaskDTO>();
            // var taskRep = new TaskRepository();

            // var addButton = new TaskDTO {Id = 1, Title = "Add button", Description = "Add a button to the page", AssignedToId = 1, State = State.New, Tags = new ReadOnlyCollection<string>(new List<string> { "1" }) };
            // var removeButton = new TaskDTO {Id = 2, Title = "Remove button", Description = "Removes a button from the page", AssignedToId = 2, State = State.New, Tags = new ReadOnlyCollection<string>(new List<string> { "1" }) };
            // var editButton = new TaskDTO {Id = 3, Title = "Edit a button", Description = "Edts a button on the page", AssignedToId = 1, State = State.New, Tags = new ReadOnlyCollection<string>(new List<string> { "1" }) };

            // var optimizeAlgorithm = new TaskDTO {Id = 4, Title = "Optimize a algorithm", Description = "Optimizes a algorithm in the backend", AssignedToId = 2, State = State.New, Tags = new ReadOnlyCollection<string>(new List<string> { "2" }) };
            // var updateDatabase = new TaskDTO {Id = 5, Title = "Update the database", Description = "Updates the database", AssignedToId = 3, State = State.New, Tags = new ReadOnlyCollection<string>(new List<string> { "2" }) };
            // var refactorStructure = new TaskDTO {Id = 6, Title = "Refactor the structure", Description = "Refactors the structure", AssignedToId = 3, State = State.New, Tags = new ReadOnlyCollection<string>(new List<string> { "2" }) };

            // col.Add(addButton);
            // col.Add(removeButton);
            // col.Add(editButton);
            // col.Add(optimizeAlgorithm);
            // col.Add(updateDatabase);
            // col.Add(refactorStructure);

            // var expected = col.AsReadOnly();

            // var result = taskRep.All();

            // var first = result.First();
            // Assert.Equal(first, expected.First());
        }
    }
}
