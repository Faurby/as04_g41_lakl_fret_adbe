using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public class Task
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public User? AssignedTo { get; set; }

        public string Description { get; set; }

        public enum EState
        {
            New,
            Active,
            Resolved,
            Closed,
            Removed
        }

        public EState State { get; set; }
        public ICollection<Tag> Tags { get; set; }

        //CONVERT ENUM TO STRING
        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.State)
                .HasConversion(
                    v => v.ToString(),
                    v => (EState)Enum.Parse(typeof(EState), v));
        }
    }


}
