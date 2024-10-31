namespace SoftkaMongo.DataAccessLayer.InterfaceMediator
{
    public interface IRequest<out TResponse> : IRequestMongo { }
    public interface IRequestMongo { }
    public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
