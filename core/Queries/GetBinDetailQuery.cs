using MediatR;
using core.Domain;

namespace core.Queries;

public record GetBinDetailQuery(Guid id) : IRequest<Bin>;