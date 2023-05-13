using MediatR;
using core.Domain;

namespace core.Queries;

public record GetItemDetailQuery(Guid id) : IRequest<Item>;