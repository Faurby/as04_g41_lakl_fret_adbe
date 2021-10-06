using System;
using System.IO;
using Assignment4.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {
        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Kanban");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            context.Database.ExecuteSqlRaw("DELETE dbo.Tags");
            context.Database.ExecuteSqlRaw("DELETE dbo.TagTask");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tasks");
            context.Database.ExecuteSqlRaw("DELETE dbo.Users");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tags', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tasks', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");

            var addButton = new Task { Title = "Add button", Description = "Add a button to the page", State = Task.EState.New };
            var removeButton = new Task { Title = "Remove button", Description = "Removes a button from the page", State = Task.EState.New };
            var editButton = new Task { Title = "Edit a button", Description = "Edts a button on the page", State = Task.EState.New };

            var ui = new Tag { Name = "ui", Tasks = new[] { addButton, removeButton, editButton } };

            addButton.Tags = new[] { ui };
            removeButton.Tags = new[] { ui };
            editButton.Tags = new[] { ui };

            var optimizeAlgorithm = new Task { Title = "Optimize a algorithm", Description = "Optimizes a algorithm in the backend", State = Task.EState.New };
            var updateDatabase = new Task { Title = "Update the database", Description = "Updates the database", State = Task.EState.New };
            var refactorStructure = new Task { Title = "Refactor the structure", Description = "Refactors the structure", State = Task.EState.New };


            var backend = new Tag { Name = "backend", Tasks = new[] { optimizeAlgorithm, updateDatabase, refactorStructure } };

            optimizeAlgorithm.Tags = new[] { backend };
            updateDatabase.Tags = new[] { backend };
            refactorStructure.Tags = new[] { backend };

            var user1 = new User { Name = "Anton", Email = "anton@hotmail.com", Tasks = new[] { addButton, editButton } };
            var user2 = new User { Name = "Frederik", Email = "Frederik@hotmail.com", Tasks = new[] { removeButton, optimizeAlgorithm } };
            var user3 = new User { Name = "Lasse", Email = "Lasse@hotmail.com", Tasks = new[] { refactorStructure, updateDatabase } };

            context.Users.AddRange(
                user1, user2, user3
            );

            context.SaveChanges();
        }
    }
}
