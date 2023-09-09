namespace BackendStressTest.Messages.Responses
{
    public class GetPersonResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Nickname { get; set; }

        public DateOnly Birthdate { get; set; }

        public string[]? Stack { get; set; }
    }
}
