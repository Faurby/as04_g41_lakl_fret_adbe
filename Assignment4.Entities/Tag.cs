using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Tag
    {
        [Key]
        public int ID {get; set;}

        [Required]
        [StringLength(50)]
        //unique!?
        public string Name {get; set;}

        public ICollection<Task> Tasks {get; set;}
    }
}
