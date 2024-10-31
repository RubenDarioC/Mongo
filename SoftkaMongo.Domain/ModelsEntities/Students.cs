namespace SoftkaMongo.Domain.ModelsEntities
{

    public class Students : BaseDocument
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

    }
}
