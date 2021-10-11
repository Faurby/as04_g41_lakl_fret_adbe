using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Assignment4.Core;
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

        public DateTime Created { get; set; }

        public DateTime StateUpdated { get; set; }  


        public string Description { get; set; }

        public State State { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}