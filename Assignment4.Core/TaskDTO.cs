using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Assignment4.Core
{
    public record TaskDTO(int Id, string Title, string AssignedToName, IReadOnlyCollection<string> Tags, State State);

    public record TaskDetailsDTO(int Id, string Title, string Description, DateTime Created, string AssignedToName, IReadOnlyCollection<string> Tags, State State, DateTime StateUpdated) : TaskDTO(Id, Title, AssignedToName, Tags, State)
    {
        public bool TestEquals(TaskDetailsDTO other)
        {
            if (Id != other.Id) return false;
            if (Title != other.Title) return false;
            if (Created.Subtract(other.Created).TotalSeconds > 5) return false;
            if (AssignedToName != other.AssignedToName) return false;
            if (Tags.Count != other.Tags.Count) return false;
            for (int i = 0; i < Tags.Count; i++)
            {
                if (Tags.ElementAt(i) != other.Tags.ElementAt(i)) return false;
            }
            if (State != other.State) return false;
            if (StateUpdated.Subtract(other.StateUpdated).TotalSeconds > 5) return false;

            return true;
        }
    }
    
    public record TaskCreateDTO
    {

        [Required]
        [StringLength(100)]
        public string Title { get; init; }

        public int? AssignedToId { get; init; }

        public string Description { get; init; }

        public ICollection<string> Tags { get; init; }
    }

    public record TaskUpdateDTO : TaskCreateDTO
    {
        public int Id { get; init; }

        public State State { get; init; }
    }
}