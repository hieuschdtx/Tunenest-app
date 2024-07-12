using MediatR;

namespace tunenest.Domain.Commons.Interfaces.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
