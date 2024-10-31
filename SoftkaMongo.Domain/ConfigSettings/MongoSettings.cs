namespace SoftkaMongo.Domain.ConfigSettings
{
    public class MongoSettings
    {
        public static string Position => "MongoSettings";
        public string DataBaseName { get; set; } = null!;
        public string Connection { get; set; } = null!;
    }
}
