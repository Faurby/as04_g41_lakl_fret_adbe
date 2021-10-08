using System;
using Assignment4.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lecture05.Entities
{
    public interface IKanbanContext : IDisposable
    {
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Tag> Tags { get; set; }
        int SaveChanges();
    }
}