namespace SoftkaMongo.Domain.ModelsEntities
{
    [EntityName("InfoCruce")]
    public class Professor : BaseDocument
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
