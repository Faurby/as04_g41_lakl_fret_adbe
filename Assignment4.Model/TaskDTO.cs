using System.Collections.Generic;

namespace Assignment4.Model
{
    public record TaskDTO
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int? AssignedToId { get; init; }
        public IReadOnlyCollection<string> Tags { get; init; }
        public State State { get; init; }
    }
}