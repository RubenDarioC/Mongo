using SoftkaMongo.Contracts.Common;

namespace SoftkaMongo.BusinessLogicLayer.Services.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
