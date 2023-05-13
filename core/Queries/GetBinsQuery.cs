using MediatR;
using core.Domain;

namespace core.Queries;

public record GetBinsQuery() : IRequest<IEnumerable<Bin>>;