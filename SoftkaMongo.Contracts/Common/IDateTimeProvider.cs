namespace SoftkaMongo.Contracts.Common
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
