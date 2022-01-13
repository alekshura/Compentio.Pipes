namespace Compentio.Pipes.WebApi.Dto
{
    public record Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
