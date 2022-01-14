namespace Compentio.Pipes.Contracts
{
    public record Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
