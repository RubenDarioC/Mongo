namespace SoftkaMongo.Domain.DataObjectTransfer
{
    public class StudentDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
