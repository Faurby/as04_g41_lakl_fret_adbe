using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class User
    {
        [Key]
        public int ID {get; set;}

        [Required]
        [StringLength(100)]
        public string Name {get; set;}

        [Required]
        [StringLength(100)]
        //make this unique?
        public string Email {get; set;}

        public ICollection<Task> Tasks {get; set;}
    }
}
