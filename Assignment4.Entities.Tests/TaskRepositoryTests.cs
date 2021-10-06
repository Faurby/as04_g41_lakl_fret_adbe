using System;
using Xunit;
using System.Collections.Generic;
using Assignment4.Core;
using System.IO;
using Assignment4.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        public void All_Returns_All_Tasks(){
            var col = new HashSet<TaskDTO>();
            var taskRep = new TaskRepository();
            
            var addButton = new TaskDTO { Title = "Add button", Description = "Add a button to the page", State = State.New };
            var removeButton = new TaskDTO { Title = "Remove button", Description = "Removes a button from the page", State = State.New };
            var editButton = new TaskDTO { Title = "Edit a button", Description = "Edts a button on the page", State = State.New };

            var optimizeAlgorithm = new TaskDTO { Title = "Optimize a algorithm", Description = "Optimizes a algorithm in the backend", State = State.New };
            var updateDatabase = new TaskDTO { Title = "Update the database", Description = "Updates the database", State = State.New };
            var refactorStructure = new TaskDTO { Title = "Refactor the structure", Description = "Refactors the structure", State = State.New };

            col.Add(addButton);
            col.Add(removeButton);
            col.Add(editButton);
            col.Add(optimizeAlgorithm);
            col.Add(updateDatabase);
            col.Add(refactorStructure);

            var result = taskRep.All();
            Assert.Equal(col, result);
        }
    }
}
