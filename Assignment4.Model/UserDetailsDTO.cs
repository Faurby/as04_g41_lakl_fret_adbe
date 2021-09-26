namespace Assignment4.Model
{
    public record UserDetailsDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public int NumberOfTasks { get; init; }
    }
}
