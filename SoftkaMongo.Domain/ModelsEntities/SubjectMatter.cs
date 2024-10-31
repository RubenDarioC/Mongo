namespace SoftkaMongo.Domain.ModelsEntities
{
    public class SubjectMatter : BaseDocument
    {
        public string Name { get; set; } = null!;
        public int Code { get; set; }
    }
}
